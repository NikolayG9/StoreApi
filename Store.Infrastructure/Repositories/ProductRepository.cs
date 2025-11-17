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

        public async Task<Product> GetByIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                                         .Include(x => x.Images)
                                         .Include(y => y.Colors)
                                         .FirstOrDefaultAsync(p => p.Id == productId);
            
            return product;
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            product.CreatedAt = DateTime.UtcNow;

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
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

            
            // Handle Images
            var existingImages = existingProduct.Images.ToList();
            
            // Remove deleted images
            foreach(var existingImage in existingImages)
            {
                if (!product.Images.Any(x => x.Id == existingImage.Id))
                {
                    dbContext.Images.Remove(existingImage);
                }
            }

            // Add or Update Images
            foreach (var image in product.Images)
            {
                var existingImage = existingImages.FirstOrDefault(x => x.Id == image.Id);
                if (existingImage == null)
                {
                    image.ProductId = existingProduct.Id;
                    existingProduct.Images.Add(image);
                }
                else
                {
                    existingImage.ImageUrl = image.ImageUrl;
                    existingImage.AltText = image.AltText;
                    existingImage.IsMain = image.IsMain;
                }
            }

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

        public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
        {
            dbContext.Remove(product);
            await dbContext.SaveChangesAsync();
        }
    }
}
