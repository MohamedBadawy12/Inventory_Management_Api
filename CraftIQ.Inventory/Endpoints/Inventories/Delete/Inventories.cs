using CraftIQ.Inventory.Services.Factories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Inventories.Delete
{
    public class Inventories(InventoryFactory<DeleteInventoryRequest, ActionResult> factory) : EndpointsAsync.WithRequest<DeleteInventoryRequest>
                                                                                                         .WithActionResult
    {
        private readonly InventoryFactory<DeleteInventoryRequest, ActionResult> _factory = factory;
        [HttpDelete(Routes.InventoriesRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(DeleteInventoryRequest request, CancellationToken cancellationToken = default)
        {

            var service = _factory.Build(nameof(Inventory));
            await service.Delete(request.inventoryId);
            return Ok("Your object has been deleted successuflly");
        }
    }
}
