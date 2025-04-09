using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Transactions.Read.ById
{
    public class ReadTransactionByIdRequest
    {
        [FromRoute]
        public Guid transactionId { get; set; }
    }
}
