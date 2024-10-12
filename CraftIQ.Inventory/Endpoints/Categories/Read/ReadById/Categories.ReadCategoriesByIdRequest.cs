using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Categories.Read.ReadById
{
    public class ReadCategoriesByIdRequest
    {
        [FromRoute]
        public Guid categoryId { get; set; }
    }
}
