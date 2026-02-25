using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Constants;

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
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> CreateCollectionAsync([FromForm] CollectionDto collectionDto, CancellationToken cancellationToken)
        {
            var createdCollection = await _collectionService.CreateAsync(collectionDto, cancellationToken);
            return Ok(createdCollection);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UpdateCollectionAsync([FromForm]CollectionDto collectionDto, CancellationToken cancellationToken)
        {
            var updatedCollection = await _collectionService.UpdateAsync(collectionDto, cancellationToken);
            return Ok(updatedCollection);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteCollectionAsync([FromRoute]int id, CancellationToken cancellationToken)
        {
            await _collectionService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
