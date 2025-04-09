using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Inventories.Delete
{
    public class DeleteInventoryRequest
    {
        [FromRoute]
        public Guid inventoryId { get; set; }
    }
}
