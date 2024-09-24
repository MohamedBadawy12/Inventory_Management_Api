using Ardalis.Specification;

namespace CraftIQ.Inventory.Core.Entities.Inventories.Specifications
{
    public class ReadInventoriesSpecification : Specification<Inventory>
    {
        public ReadInventoriesSpecification()
        {
            Query.Where(o => o.Id != 0);
        }
    }
}
