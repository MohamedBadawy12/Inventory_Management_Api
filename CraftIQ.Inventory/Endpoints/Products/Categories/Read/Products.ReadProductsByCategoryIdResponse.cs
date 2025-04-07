using CraftIQ.Inventory.Endpoints.Products.Read;
using CraftIQ.Inventory.Shared.Contracts.Products;

namespace CraftIQ.Inventory.Endpoints.Products.Categories.Read
{
    public class ReadProductsByCategoryIdResponse : ReadProductsResponse
    {
        public Guid CategoryId { get; set; }
        public ReadProductsByCategoryIdResponse(ProductsContract contract) : base(contract)
        {
            CategoryId = contract.CategoryId;
        }
    }
}
