using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Transactions.Read
{
    public class Transactions(InventoryFactory<dynamic, TransactionsContract> factory) : EndpointsAsync.WithoutRequest
                                                                                                 .WithActionResult<ReadTransactionResponse>
    {
        private readonly InventoryFactory<dynamic, TransactionsContract> _factory = factory;
        [HttpGet(Routes.TransactionsRoutes.BaseUrl)]
        public override async Task<ActionResult<ReadTransactionResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var service = _factory.Build(nameof(Transaction));
            var oData = await service.Read();
            var oResult = oData.Select(o => new ReadTransactionResponse(o)).ToList();
            return Ok(oResult);
        }
    }
}
