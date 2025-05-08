namespace CraftIQ.Inventory.Shared.Contracts.OrderDetails
{
    public class OrderDetailsOperationsContract
    {
        public Guid OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        public OrderDetailsOperationsContract(Guid orderDetailId,
                                              int quantity,
                                              decimal totalPrice,
                                              Guid orderId,
                                              Guid productId)
        {
            OrderDetailId = orderDetailId;
            Quantity = quantity;
            TotalPrice = totalPrice;
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
