using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Core.Entities.Transactions.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CraftIQ.Inventory.Services.TransactionsImplementaions
{
    public class TransactionsServices<TRequest, TResponse>(IRepository<Transaction> transactionRepository) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Transaction> _transactionRepository = transactionRepository;
        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContarct = contract as TransactionsOperationsContract;
            var oTransaction = new Transaction(oContarct!.EmployeeId,
                                                oContarct.TransactionDate,
                                                oContarct.Quantity,
                                                oContarct.TransactionType,
                                                oContarct.Notes);
            var oTransactionResult = await _transactionRepository.AddAsync(oTransaction);
            if (oTransactionResult == null)
                return default!;

            oContarct.TransactionId = oTransactionResult.TransactionId;
            return oContarct as dynamic;
        }

        public async ValueTask Delete(Guid ContractId)
        {
            var oReadByIdSpec = new ReadTransactionsByIdSpecification(ContractId);
            var oResult = await _transactionRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                await _transactionRepository.DeleteAsync(oResult);
            else throw new ResultException("You can't delete an object that does not exist.", (int)HttpStatusCode.Forbidden);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadSpec = new ReadTransactionsSpecification();
            var oData = await _transactionRepository.ListAsync(oReadSpec);
            if (oData != null && oData.Count > 0)
            {
                var oResult = oData.Select(o => new TransactionsContract(o.TransactionId,
                                                                         o.EmployeeId,
                                                                         o.TransactionDate,
                                                                         o.Quantity,
                                                                         o.TransactionType,
                                                                         o.Notes,
                                                                         o.CreatedBy,
                                                                         o.ModifiedBy,
                                                                         o.CreatedOn,
                                                                         o.ModifiedOn)).ToList();
                return oResult as dynamic;
            }
            else return new List<TransactionsContract>() as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid ContractId)
        {
            var oReadByIdSpec = new ReadTransactionsByIdSpecification(ContractId);
            var oResult = await _transactionRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                return new TransactionsContract(oResult.TransactionId,
                                                                         oResult.EmployeeId,
                                                                         oResult.TransactionDate,
                                                                         oResult.Quantity,
                                                                         oResult.TransactionType,
                                                                         oResult.Notes,
                                                                         oResult.CreatedBy,
                                                                         oResult.ModifiedBy,
                                                                         oResult.CreatedOn,
                                                                         oResult.ModifiedOn) as dynamic;
            else throw new ResultException("This object does not exist", (int)HttpStatusCode.NotFound);
        }
        public async ValueTask Update(Guid ContractId, TRequest contract)
        {
            var oContract = contract as TransactionsOperationsContract;
            var oReadByIdSpec = new ReadTransactionsByIdSpecification(ContractId);
            var oResult = await _transactionRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                oResult.UpdateTransaction(oContract!);
                await _transactionRepository.UpdateAsync(oResult);
            }
            else throw new ResultException("This object does not exist", (int)HttpStatusCode.NotFound);
        }


        public async ValueTask<List<TResponse>> ReadByParentId(Guid parentContractId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<TResponse> ReadSingleByParentId(Guid contractId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }


        public async ValueTask UpdateParentId(Guid ContractId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }
    }
}
