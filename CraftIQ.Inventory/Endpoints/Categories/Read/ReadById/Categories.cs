using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Categories.Read.ReadById
{
    /// <summary>
    /// My Name Mohamed Badawy
    /// In this endpoint is about Read category by id using InventoryFactory 
    /// </summary>
    public class Categories(InventoryFactory<dynamic, CategoriesContract> factory) : EndpointsAsync.WithRequest<ReadCategoriesByIdRequest>
                                                                        .WithActionResult<ReadCategoriesByIdResponse>
    {
        private readonly InventoryFactory<dynamic, CategoriesContract> _factory = factory;
        [HttpGet(Routes.CategoriesRoutes.ReadById)]
        public override async Task<ActionResult<ReadCategoriesByIdResponse>> HandleAsync(ReadCategoriesByIdRequest request, CancellationToken cancellationToken = default)
        {
            var service = _factory.Build(nameof(Category));
            var oData = await service.ReadById(request.categoryId);
            var oResult = new ReadCategoriesByIdResponse(oData);
            return Ok(oResult);
        }
    }
}
