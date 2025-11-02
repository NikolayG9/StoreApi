using Microsoft.AspNetCore.Mvc;
using Store.Application.Collections;
using Store.Application.Collections.Dtos;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/collections")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCollectionsAsync(CancellationToken cancellationToken)
        {
            var collections = await _collectionService.GetAllAsync(cancellationToken);
            return Ok(collections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollectionByIdAsync([FromRoute]int id, CancellationToken cancellationToken)
        {
            var collection = await _collectionService.GetByIdAsync(id, cancellationToken);
            return Ok(collection);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CollectionDto collectionDto, CancellationToken cancellationToken)
        {
            var createdCollection = await _collectionService.CreateAsync(collectionDto, cancellationToken);
            return Ok(createdCollection);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollectionAsync([FromBody]CollectionDto collectionDto, CancellationToken cancellationToken)
        {
            var updatedCollection = await _collectionService.UpdateAsync(collectionDto, cancellationToken);
            return Ok(updatedCollection);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCollectionAsync([FromRoute]int id, CancellationToken cancellationToken)
        {
            var isCollectionDeleted = await _collectionService.DeleteAsync(id, cancellationToken);
            return Ok(isCollectionDeleted);
        }
    }
}
