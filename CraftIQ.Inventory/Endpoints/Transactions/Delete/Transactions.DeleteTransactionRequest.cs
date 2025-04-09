using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Transactions.Delete
{
    public class DeleteTransactionRequest
    {
        [FromRoute]
        public Guid transactionId { get; set; }
    }
}
