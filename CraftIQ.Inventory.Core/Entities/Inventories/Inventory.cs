using CraftIQ.Inventory.Core.Entities.Products;

namespace CraftIQ.Inventory.Core.Entities.Inventories
{
    public class Inventory : BaseEntity
    {
        //public Inventory()// for entity framework
        //{
        //}
        public Guid InventoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Reorderlevel { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTimeOffset LastUpdated { get; set; }

        // relation with products
        public List<Product> Products { get; set; } = new();
        //public Inventory(int quantity, int reorderlevel, string location,
        //    DateTimeOffset lastUpdated, string name)
        //{
        //    InventoryId = Guid.NewGuid();
        //    Quantity = quantity;
        //    Reorderlevel = reorderlevel;
        //    Location = location;
        //    LastUpdated = lastUpdated;
        //    Name = name;
        //    CreatedBy = new();
        //    CreatedOn = DateTimeOffset.Now;
        //    ModifiedBy = new();
        //    ModifiedOn = DateTimeOffset.Now;
        //}
        //public void UpdateInventory()
        //{

        //}
    }
}
