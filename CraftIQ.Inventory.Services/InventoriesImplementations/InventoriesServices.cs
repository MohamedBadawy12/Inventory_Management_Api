using CraftIQ.Inventory.Core.Entities.Inventories.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CraftIQ.Inventory.Services.InventoriesImplementations
{
    public class InventoriesServices<TRequest, TResponse>(IRepository<Core.Entities.Inventories.Inventory> repository) : IGenericServices<TRequest, TResponse>
    {
        IRepository<Core.Entities.Inventories.Inventory> _repository = repository;

        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContract = contract as InventoriesOperationsContract;
            var oData = new Core.Entities.Inventories.Inventory(oContract!.Quantity,
                                                                oContract.Reorderlevel,
                                                                oContract.Location,
                                                                oContract.LastUpdated,
                                                                oContract.Name);
            var oResult = await _repository.AddAsync(oData);
            if (oResult != null)
                return default;

            oContract.InventoryId = oResult.InventoryId;
            return oContract as dynamic;
        }

        public async ValueTask Delete(Guid ContractId)
        {
            var oReadByIdSpec = new ReadInventoryByIdSpecification(ContractId);
            var oResult = await _repository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                await _repository.DeleteAsync(oResult);
            else throw new ResultException("You can't delete object that isn't exit", (int)HttpStatusCode.Forbidden);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadSpec = new ReadInventoriesSpecification();
            var oData = await _repository.ListAsync(oReadSpec);
            if (oData != null && oData.Count > 0)
            {
                var oResult = oData.Select(o => new InventoriesContract(o.InventoryId,
                                                                       o.Name,
                                                                       o.Quantity,
                                                                       o.Reorderlevel,
                                                                       o.Location,
                                                                       o.CreatedBy,
                                                                       o.ModifiedBy,
                                                                       o.CreatedOn,
                                                                       o.ModifiedOn)).ToList();
                return oResult as dynamic;
            }
            else return new List<InventoriesContract>() as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid ContractId)
        {
            var oReadByIdSpec = new ReadInventoryByIdSpecification(ContractId);
            var oResult = await _repository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                return new InventoriesContract(oResult.InventoryId,
                                                                       oResult.Name,
                                                                       oResult.Quantity,
                                                                       oResult.Reorderlevel,
                                                                       oResult.Location,
                                                                       oResult.CreatedBy,
                                                                       oResult.ModifiedBy,
                                                                       oResult.CreatedOn,
                                                                       oResult.ModifiedOn) as dynamic;
            }
            else throw new ResultException("This object is not exit", (int)HttpStatusCode.NotFound);
        }
        public async ValueTask Update(Guid ContractId, TRequest contract)
        {
            var oContract = contract as InventoriesOperationsContract;
            var oReadByIdSpec = new ReadInventoryByIdSpecification(oContract.InventoryId);
            var oResult = await _repository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                oResult.UpdateInventory(oContract!);
                await _repository.UpdateAsync(oResult);
            }
            else throw new ResultException("This object is not exit", (int)HttpStatusCode.NotFound);
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
