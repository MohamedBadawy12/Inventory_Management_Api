using Ardalis.Specification;

namespace CraftIQ.Inventory.Core.Entities.Products.Specifications
{
    public class ReadProductsByIdSpecification : SingleResultSpecification<Product>
    {
        public ReadProductsByIdSpecification(Guid productId)
        {
            Query.Where(o => o.ProductId == productId)
                  .Include(o => o.Inventory)
                  .Include(o => o.Transaction);
        }
    }
}
