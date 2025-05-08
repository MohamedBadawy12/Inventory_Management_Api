using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.Categories.Specifications;
using CraftIQ.Inventory.Core.Entities.Inventories.Specifications;
using CraftIQ.Inventory.Core.Entities.Products;
using CraftIQ.Inventory.Core.Entities.Products.Specifications;
using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Core.Entities.Transactions.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Products;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CraftIQ.Inventory.Services.ProductsImplementations
{
    public class ProductsServices<TRequest, TResponse>(IRepository<Category> categoryRepository,
        IRepository<Product> productRepository, IRepository<Core.Entities.Inventories.Inventory> inventoryRepository,
                  IRepository<Transaction> transactionRepository) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Category> _categoryRepository = categoryRepository;
        private readonly IRepository<Product> _productRepository = productRepository;
        private readonly IRepository<Core.Entities.Inventories.Inventory> _inventoryRepository = inventoryRepository;
        private readonly IRepository<Transaction> _transactionRepository = transactionRepository;
        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContract = contract as ProductsOperationsContract;

            //Category
            var oCategoryReadByIdSpec = new ReadCategoryByIdSpecification(oContract!.CategoryId);
            var oCategory = await _categoryRepository.FirstOrDefaultAsync(oCategoryReadByIdSpec);
            if (oCategory == null)
                throw new ResultException("you can't add product in category that not exist!", (int)HttpStatusCode.NotFound);

            //Inventory
            var oInventoryReadByIdSpec = new ReadInventoryByIdSpecification(oContract!.InventoryId);
            var oInventory = await _inventoryRepository.FirstOrDefaultAsync(oInventoryReadByIdSpec);
            if (oInventory == null)
                throw new ResultException("you can't add product that not exist in an inventory!", (int)HttpStatusCode.NotFound);

            //Transaction
            var oTransactionReadByIdSpec = new ReadTransactionsByIdSpecification(oContract!.TransactionId);
            var oTransaction = await _transactionRepository.FirstOrDefaultAsync(oTransactionReadByIdSpec);
            if (oTransaction == null)
                throw new ResultException("you must start a transaction before you add product!", (int)HttpStatusCode.NotFound);

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
            oProducts.SetInventory(oInventory);
            oProducts.SetTransaction(oTransaction);

            var oProductResult = await _productRepository.AddAsync(oProducts);
            if (oProductResult == null)
                return default;
            oContract.ProductId = oProductResult.ProductId;
            return oContract as dynamic;
        }

        public async ValueTask Delete(Guid ProductId)
        {
            var oReadByIdSpec = new ReadProductsByIdSpecification(ProductId);
            var oResult = await _productRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                await _productRepository.DeleteAsync(oResult);
            else throw new ResultException("You can't delete object that is not exist.", (int)HttpStatusCode.Forbidden);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadSpec = new ReadProductsSpecification();
            var oData = await _productRepository.ListAsync(oReadSpec);
            if (oData != null && oData.Count > 0)
            {
                var oResult = oData.Select(o => new ProductsContract(o.ProductId,
                                                                    Guid.Empty,
                                                                    o.Inventory.InventoryId,
                                                                    o.Transaction.TransactionId,
                                                                    o.Name,
                                                                    o.Description,
                                                                    o.UnitPrice,
                                                                    o.Weight,
                                                                    o.Length,
                                                                    o.Width,
                                                                    o.Height,
                                                                    o.TaxCost,
                                                                    o.ProfitPerUnit,
                                                                    o.ProductionCost)).ToList();
                return oResult as dynamic;
            }
            else return new List<ProductsContract>() as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid ProductId)
        {
            var oReadByIdSpec = new ReadProductsByIdSpecification(ProductId);
            var oResult = await _productRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                return new ProductsContract(oResult.ProductId,
                                            Guid.Empty,
                                            oResult.Inventory.InventoryId,
                                            oResult.Transaction.TransactionId,
                                            oResult.Name,
                                            oResult.Description,
                                            oResult.UnitPrice,
                                            oResult.Weight,
                                            oResult.Length,
                                            oResult.Width,
                                            oResult.Height,
                                            oResult.TaxCost,
                                            oResult.ProfitPerUnit,
                                            oResult.ProductionCost,
                                            oResult.CreatedBy,
                                            oResult.ModifiedBy,
                                            oResult.CreatedOn,
                                            oResult.ModifiedOn,
                                            oResult.Inventory.Quantity,
                                            oResult.Inventory.Reorderlevel) as dynamic;

            else throw new ResultException("This object is not exist", (int)HttpStatusCode.NotFound);
        }

        public async ValueTask<List<TResponse>> ReadByParentId(Guid parentContractId)
        {
            var oReadCategoriesByIdSpec = new ReadCategoryByIdSpecification(parentContractId);
            var oCategoryResult = await _categoryRepository.FirstOrDefaultAsync(oReadCategoriesByIdSpec);
            if (oCategoryResult != null)
            {
                var oProduct = oCategoryResult.Products;
                var oResult = oProduct.Select(o => new ProductsContract(o.ProductId,
                                                                         oCategoryResult.CategoryId,
                                                                         Guid.Empty,
                                                                         Guid.Empty,
                                                                         o.Name,
                                                                         o.Description,
                                                                         o.UnitPrice,
                                                                         o.Weight,
                                                                         o.Length,
                                                                         o.Width,
                                                                         o.Height,
                                                                         o.TaxCost,
                                                                         o.ProfitPerUnit,
                                                                         o.ProductionCost)).ToList();
                return oResult as dynamic;
            }
            return new List<ProductsContract>() as dynamic;

        }

        public async ValueTask<TResponse> ReadSingleByParentId(Guid ProductId, Guid parentContractId)
        {
            var oReadSingleProductByCategoryIdSpec = new ReadSingleProductByCategoryIdSpecification(ProductId, parentContractId);
            var oCategoryResult = await _categoryRepository.FirstOrDefaultAsync(oReadSingleProductByCategoryIdSpec);
            if (oCategoryResult != null)
            {
                if (oCategoryResult.Products == null || oCategoryResult.Products.Count == 0)
                    throw new ResultException("This object is not exist", (int)HttpStatusCode.NotFound);
                var oProduct = oCategoryResult.Products.FirstOrDefault();
                var oResult = new ProductsContract(oProduct!.ProductId,
                                                                         oCategoryResult.CategoryId,
                                                                         Guid.Empty,
                                                                         Guid.Empty,
                                                                         oProduct.Name,
                                                                         oProduct.Description,
                                                                         oProduct.UnitPrice,
                                                                         oProduct.Weight,
                                                                         oProduct.Length,
                                                                         oProduct.Width,
                                                                         oProduct.Height,
                                                                         oProduct.TaxCost,
                                                                         oProduct.ProfitPerUnit,
                                                                         oProduct.ProductionCost);
                return oResult as dynamic;
            }
            return default;
        }

        public async ValueTask Update(Guid ProductId, TRequest contract)
        {
            var oContract = contract as ProductsOperationsContract;
            var oReadByIdSpec = new ReadProductsByIdSpecification(ProductId);
            var oResult = await _productRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                oResult.UpdateProduct(oContract!);
                await _productRepository.UpdateAsync(oResult);
            }
            else throw new ResultException("This object is not exist", (int)HttpStatusCode.NotFound);
        }

        public async ValueTask UpdateParentId(Guid ProductId, Guid parentContractId)
        {
            var oReadByIdSpec = new ReadProductsByIdSpecification(ProductId);
            var oResult = await _productRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult == null)
                throw new ResultException("This product is not exist", (int)HttpStatusCode.NotFound);
            var OReadParentByIdSpec = new ReadCategoryByIdSpecification(parentContractId);
            var oParentResult = await _categoryRepository.FirstOrDefaultAsync(OReadParentByIdSpec);
            if (oParentResult == null)
                throw new ResultException("This category object is not exist", (int)HttpStatusCode.NotFound);
            oResult.SetCategory(oParentResult);
            await _productRepository.UpdateAsync(oResult);
        }
    }
}
