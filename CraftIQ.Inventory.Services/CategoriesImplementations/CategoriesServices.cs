using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.Categories.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CraftIQ.Inventory.Services.CategoriesImplementations
{
    public class CategoriesServices<TRequest, TResponse>(IRepository<Category> repository) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Category> _repository = repository;

        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContract = contract as CategoriesOperationsContract;
            var oData = new Category(oContract!.Name, oContract.Description);
            var oResult = await _repository.AddAsync(oData);
            return new CategoriesOperationsContract(oResult.Name, oResult.Description) as dynamic;
        }

        public async ValueTask Delete(Guid ContractId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadSpec = new ReadCategoriesSpecification();
            var oData = await _repository.ListAsync(oReadSpec);
            if (oData != null && oData.Count > 0)
            {
                var oResult = oData.Select(o => new CategoriesContract(o.CategoryId, o.Name,
                    o.Description, o.CreatedBy, o.ModifiedBy, o.CreatedOn, o.ModifiedOn)).ToList();
                return oResult as dynamic;
            }
            else return new List<CategoriesContract>() as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid CategorytId)
        {
            var oReadByIdSpec = new ReadCategoryByIdSpecification(CategorytId);
            var oResult = await _repository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                return new CategoriesContract(oResult.CategoryId, oResult.Name,
                    oResult.Description, oResult.CreatedBy, oResult.ModifiedBy,
                    oResult.CreatedOn, oResult.ModifiedOn) as dynamic;
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

        public async ValueTask Update(Guid ContractId, TRequest contract)
        {
            throw new NotImplementedException();
        }

        public async ValueTask UpdateParentId(Guid ContractId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }
    }
}
