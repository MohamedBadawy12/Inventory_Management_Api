using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.OrderDetails;
using CraftIQ.Inventory.Core.Entities.Transactions;


namespace CraftIQ.Inventory.Core.Entities.Products
{
    public class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public decimal TaxCost { get; set; }
        public decimal ProfitPerUnit { get; set; }
        public decimal ProductionCost { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new();
        //relation with Inventory
        public int InventoryId { get; set; }
        public Core.Entities.Inventories.Inventory Inventory { get; set; } = new();

        //relation with Transactions
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = new();

        // relation with order details
        public List<OrderDetail> OrderDetails { get; set; } = new();
        //public Product(string name, string description, decimal unitPrice, float weight, float length,
        //    float width, float height, decimal taxCost, decimal profitPerUnit, decimal productionCost)
        //{
        //    ProductId = Guid.NewGuid();
        //    Name = name;
        //    Description = description;
        //    UnitPrice = unitPrice;
        //    Weight = weight;
        //    Length = length;
        //    Width = width;
        //    Height = height;
        //    TaxCost = taxCost;
        //    ProfitPerUnit = profitPerUnit;
        //    ProductionCost = productionCost;
        //    CreatedBy = new();
        //    CreatedOn = DateTimeOffset.Now;
        //    ModifiedBy = new();
        //    ModifiedOn = DateTimeOffset.Now;
        //}

        //public void SetCategory(Category category) =>
        //    Category = category;

        //public void SetInventory(Inventories.Inventory inventory) =>
        //    Inventory = inventory;

        //public void SetTransaction(Transaction transaction) =>
        //    Transaction = transaction;
    }
}
