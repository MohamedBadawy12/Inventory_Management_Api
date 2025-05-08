using CraftIQ.Inventory.Core.Entities.OrderDetails;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.OrderDetails.Read
{
    public class OrderDetails(InventoryFactory<dynamic, OrderDetailsContract> factory) : EndpointsAsync.WithoutRequest
                                                                                                  .WithActionResult<ReadOrderDetailsResponse>
    {
        private readonly InventoryFactory<dynamic, OrderDetailsContract> _factory = factory;
        [HttpGet(Routes.OrderDetailsRoutes.BaseUrl)]
        public override async Task<ActionResult<ReadOrderDetailsResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var servcie = _factory.Build(nameof(OrderDetail));
            var oData = await servcie.Read();
            var oResult = oData.Select(o => new ReadOrderDetailsResponse(o)).ToList();
            return Ok(oResult);
        }
    }
}
