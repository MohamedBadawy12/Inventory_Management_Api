using System.ComponentModel.DataAnnotations;

namespace CraftIQ.Inventory.Endpoints.Categories.Create
{
    public class CreateCategoriesResponse
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public CreateCategoriesResponse(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
