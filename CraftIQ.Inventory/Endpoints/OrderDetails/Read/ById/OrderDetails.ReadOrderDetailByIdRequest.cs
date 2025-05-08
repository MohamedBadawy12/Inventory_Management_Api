using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.OrderDetails.Read.ById
{
    public class ReadOrderDetailByIdRequest
    {
        [FromRoute]
        public Guid orderDetailId { get; set; }
    }
}
