using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Services.Factories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Categories.Delete
{
    /// <summary>
    /// My Name Mohamed Badawy
    /// In this endpoint is about Delete category using InventoryFactory
    /// </summary>
    public class Categories(InventoryFactory<CategoriesDeleteRequest, ActionResult> factory) : EndpointsAsync.WithRequest<CategoriesDeleteRequest>
                                                                                                .WithActionResult
    {
        private readonly InventoryFactory<CategoriesDeleteRequest, ActionResult> _factory = factory;

        [HttpDelete(Routes.CategoriesRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(CategoriesDeleteRequest request, CancellationToken cancellationToken = default)
        {
            var service = _factory.Build(nameof(Category));
            await service.Delete(request.categoryId);
            return Ok("Your object is deleted successfully");
        }
    }
}
