using Ardalis.Specification;
namespace CraftIQ.Inventory.Core.Entities.Categories.Specifications
{
    public class ReadCategoryByIdSpecification : SingleResultSpecification<Category>
    {
        public ReadCategoryByIdSpecification(Guid categoryId)
        {
            Query.Where(o => o.CategoryId == categoryId);
        }
    }
}
