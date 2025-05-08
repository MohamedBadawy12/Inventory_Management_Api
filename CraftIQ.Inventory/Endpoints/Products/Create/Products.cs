using CraftIQ.Inventory.Core.Entities.Products;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Products;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CraftIQ.Inventory.Endpoints.Products.Create
{
    public class Products(InventoryFactory<ProductsOperationsContract, ProductsContract> factory) : EndpointsAsync.WithRequest<CreateProductRequest>
                                                                                                  .WithActionResult<CreateProductResponse>
    {
        private readonly InventoryFactory<ProductsOperationsContract, ProductsContract> _factory = factory;
        [HttpPost(Routes.ProductsRoutes.BaseUrl)]
        public override async Task<ActionResult<CreateProductResponse>> HandleAsync([FromBody] CreateProductRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("request can't be null", (int)HttpStatusCode.BadRequest);
            var service = _factory.Build(nameof(Product));
            var oData = new ProductsOperationsContract(Guid.Empty,
                                                        request.CategoryId,
                                                        request.InventoryId,
                                                        request.TransactionId,
                                                        request.Name,
                                                        request.Description,
                                                        request.UnitPrice,
                                                        request.Weight,
                                                        request.Length,
                                                        request.Width,
                                                        request.Height,
                                                        request.TaxCost,
                                                        request.ProfitPerUnit,
                                                        request.ProductionCost);
            var oCreateProduct = await service.Create(oData);
            return Ok(new CreateProductResponse(oCreateProduct.ProductId));
        }
    }
}
