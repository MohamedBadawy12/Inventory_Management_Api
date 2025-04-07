using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Products.Categories.Update
{
    public class UpdateProductCategoryRequest
    {
        [FromRoute]
        public Guid productId { get; set; }
        [FromBody]
        public UpdateProduct Product { get; set; } = null!;
    }
    public class UpdateProduct
    {
        public Guid CategoryId { get; set; }
    }
}
