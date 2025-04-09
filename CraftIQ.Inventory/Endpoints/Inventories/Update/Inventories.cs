using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Inventories.Update
{
    public class Inventories(InventoryFactory<InventoriesOperationsContract, dynamic> factory) : EndpointsAsync.WithRequest<UpdateInventoryRequest>
                                                                                                            .WithActionResult
    {
        private readonly InventoryFactory<InventoriesOperationsContract, dynamic> _factory = factory;
        [HttpPut(Routes.InventoriesRoutes.Update)]
        public override async Task<ActionResult> HandleAsync(UpdateInventoryRequest request, CancellationToken cancellationToken = default)
        {
            var service = _factory.Build(nameof(Inventory));
            var oData = new InventoriesOperationsContract(request.inventoryId,
                                                          request.Inventory.Name,
                                                          request.Inventory.Quantity,
                                                          request.Inventory.Reorderlevel,
                                                          request.Inventory.Location);
            await service.Update(request.inventoryId, oData);
            return Ok("Your object has been updated successfully");
        }
    }
}
