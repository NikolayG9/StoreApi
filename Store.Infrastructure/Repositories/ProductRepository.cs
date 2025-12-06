using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    internal class ProductRepository(StoreDbContext dbContext) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetByCollectionIdAsync(int collectionId, CancellationToken cancellationToken)
        {
            var products = await dbContext.Products
                                    .Where(x => x.CollectionId == collectionId)
                                    .Include(y => y.Images.Where(i => i.IsMain == true))
                                    .ToListAsync();
            return products;
        }

        public async Task<(IEnumerable<Product>, int)> GetByCollectionIdWithParamsAsync(int collectionId, string? searchPhrase, int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext.Products.Where(x => x.CollectionId == collectionId && searchPhrase == null || (x.Name.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            var products = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            
            return (products, totalCount);
        }

        public async Task<IEnumerable<Product>> GetSoftDeletedProductsByCollectionIdAsync(int collectionId, CancellationToken cancellationToken)
        {
            var products = await dbContext.Products
                        .Where(x => x.CollectionId == collectionId)
                        .Include(y => y.Images.Where(i => i.IsMain == true))
                        .ToListAsync();
            
            return products;
        }

        public async Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                                         .Include(x => x.Images)
                                         .Include(y => y.Colors)
                                         .FirstOrDefaultAsync(p => p.Id == productId);
            
            return product;
        }

        public async Task<bool> IsAnyProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            return await dbContext.Products.AnyAsync(x => x.Id == productId, cancellationToken);
        }

        public async Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            product.CreatedAt = DateTime.UtcNow;

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return product;
        }

        public async Task AddProductImageAsync(Image image, CancellationToken cancellationToken)
        {
            await dbContext.Images.AddAsync(image, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            var existingProduct = await dbContext.Products
                                                 .Include(x => x.Images)
                                                 .Include(y => y.Colors)
                                                 .FirstAsync(x => x.Id == product.Id);

            // Update properties
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.ProductType = product.ProductType;
            existingProduct.Price = product.Price;
            existingProduct.Discount = product.Discount;
            existingProduct.CollectionId = product.CollectionId;

            // Handle Colors
            var existingColors = existingProduct.Colors.ToList();

            // Remove deleted colors
            foreach (var existingColor in existingColors)
            {
                if (!product.Colors.Any(x => x.Id == existingColor.Id))
                {
                    dbContext.Colors.Remove(existingColor);
                }
            }

            // Add or Update Colors
            foreach(var color in product.Colors)
            {
                var existingColor = existingColors.FirstOrDefault(x => x.Id == color.Id);
                if (existingColor == null)
                {
                    color.ProductId = existingProduct.Id;
                    existingProduct.Colors.Add(color);
                }
                else
                {
                    existingColor.Name = color.Name;
                    existingColor.HexColorCode = color.HexColorCode;
                }
            }

            await dbContext.SaveChangesAsync();

            return existingProduct;
        }

        public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken)
        {
            dbContext.Remove(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductImageAsync(Image image, CancellationToken cancellationToken)
        {
            dbContext.Remove(image);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
