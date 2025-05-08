using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Orders.Delete
{
    public class DeleteOrderRequest
    {
        [FromRoute]
        public Guid orderId { get; set; }
    }
}
