namespace CraftIQ.Inventory.Endpoints.Transactions.Create
{
    public class CreateTransactionResponse
    {
        public Guid TransactionId { get; set; }

        public CreateTransactionResponse(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
