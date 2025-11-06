using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/collections/{collectionId}/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByCollectionIdAsync([FromRoute]int collectionId, CancellationToken cancellationToken)
        {
            var result = await _productService.GetByCollectionIdAsync(collectionId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductDetailsByIdAsync([FromRoute]int productId, CancellationToken cancellationToken)
        {
            var result = await _productService.GetByIdAsync(productId, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody]ProductDto productDto, CancellationToken cancellationToken)
        {
            var result = await _productService.CreateAsync(productDto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync([FromBody]ProductDto productDto, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateAsync(productDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute]int productId, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteAsync(productId, cancellationToken);
            return Ok(result);
        }
    }
}
