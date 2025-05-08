using CraftIQ.Inventory.Core.Entities.OrderDetails;
using CraftIQ.Inventory.Services.Factories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.OrderDetails.Delete
{
    public class OrderDetails(InventoryFactory<DeleteOrderDetailRequest, ActionResult> factory) : EndpointsAsync.WithRequest<DeleteOrderDetailRequest>
                                                                                                    .WithActionResult
    {
        private readonly InventoryFactory<DeleteOrderDetailRequest, ActionResult> _factory = factory;
        [HttpDelete(Routes.OrderDetailsRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(DeleteOrderDetailRequest request, CancellationToken cancellationToken = default)
        {
            var servcie = _factory.Build(nameof(OrderDetail));
            await servcie.Delete(request.orderDetailId);
            return Ok("Your object has been deleted successfully");
        }
    }
}
