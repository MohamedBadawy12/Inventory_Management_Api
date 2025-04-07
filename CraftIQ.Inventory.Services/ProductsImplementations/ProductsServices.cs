using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.Categories.Specifications;
using CraftIQ.Inventory.Core.Entities.Products;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Products;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CraftIQ.Inventory.Services.ProductsImplementations
{
    public class ProductsServices<TRequest, TResponse>(IRepository<Category> categoryRepository,
        IRepository<Product> productRepository) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Category> _categoryRepository = categoryRepository;
        private readonly IRepository<Product> _productRepository = productRepository;

        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContract = contract as ProductsOperationsContract;
            var oCategoryReadByIdSpec = new ReadCategoryByIdSpecification(oContract!.CategoryId);
            var oCategory = await _categoryRepository.FirstOrDefaultAsync(oCategoryReadByIdSpec);
            if (oCategory == null)
                throw new ResultException("you can't add product in category that not exist!", (int)HttpStatusCode.NotFound);

            var oProducts = new Product(oContract.Name,
                                        oContract.Description,
                                        oContract.UnitPrice,
                                        oContract.Weight,
                                        oContract.Length,
                                        oContract.Width,
                                        oContract.Height,
                                        oContract.TaxCost,
                                        oContract.ProfitPerUnit,
                                        oContract.ProductionCost);
            oProducts.SetCategory(oCategory);

            var oProductResult = await _productRepository.AddAsync(oProducts);
            if (oProductResult == null)
                return default;
            oContract.ProductId = oProductResult.ProductId;
            return oContract as dynamic;
        }

        public async ValueTask Delete(Guid ProductId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<List<TResponse>> Read()
        {
            throw new NotImplementedException();
        }

        public async ValueTask<TResponse> ReadById(Guid ProductId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<List<TResponse>> ReadByParentId(Guid parentContractId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<TResponse> ReadSingleByParentId(Guid ProductId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask Update(Guid ProductId, TRequest contract)
        {
            throw new NotImplementedException();
        }

        public async ValueTask UpdateParentId(Guid ProductId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }
    }
}
