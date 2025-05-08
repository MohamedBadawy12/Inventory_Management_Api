using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.OrderDetails.Delete
{
    public class DeleteOrderDetailRequest
    {
        [FromRoute]
        public Guid orderDetailId { get; set; }
    }
}
