using CraftIQ.Inventory.Core.Entities.OrderDetails;

namespace CraftIQ.Inventory.Core.Entities.Orders
{
    public class Order : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid SupplierId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTimeOffset ExpectedDeliveryDate { get; set; }
        public int OrderType { get; set; }
        public DateTimeOffset ReceivedDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();

        //public Order() { } // ctor for ef core
        //public Order(Guid supplierId, int totalAmount, int status,
        //    DateTimeOffset expectedDeliveryDate, int orderType)
        //{
        //    OrderId = Guid.NewGuid();
        //    SupplierId = supplierId;
        //    TotalAmount = totalAmount;
        //    Status = status;
        //    ExpectedDeliveryDate = expectedDeliveryDate;
        //    OrderType = orderType;
        //    CreatedBy = new();
        //    CreatedOn = DateTimeOffset.Now;
        //    ModifiedBy = new();
        //    ModifiedOn = DateTimeOffset.Now;
        //}
        //public void UpdateDeliveryDate(DateTimeOffset receivedDate) =>
        //   ReceivedDate = receivedDate;

        //public void AddOrderDetails(OrderDetail orderDetail) =>
        //    OrderDetails.Add(orderDetail);
    }
}
