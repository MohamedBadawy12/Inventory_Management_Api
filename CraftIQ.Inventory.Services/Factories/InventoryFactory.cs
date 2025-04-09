using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.Products;
using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.CategoriesImplementations;
using CraftIQ.Inventory.Services.InventoriesImplementations;
using CraftIQ.Inventory.Services.ProductsImplementations;
using CraftIQ.Inventory.Services.TransactionsImplementaions;
using huzcodes.Persistence.Interfaces.Repositories;

namespace CraftIQ.Inventory.Services.Factories
{
    public class InventoryFactory<TRequest, TResponse>(IRepository<Category> CategoryRepository
        , IRepository<Product> ProductRepository, IRepository<Core.Entities.Inventories.Inventory> inventoryRepository,
        IRepository<Transaction> TransactionRepository)
    {
        private readonly IRepository<Category> _CategoryRepository = CategoryRepository;
        private readonly IRepository<Product> _ProductRepository = ProductRepository;
        private readonly IRepository<Core.Entities.Inventories.Inventory> _inventoryRepository = inventoryRepository;
        private readonly IRepository<Transaction> _TransactionRepository = TransactionRepository;
        public IGenericServices<TRequest, TResponse> Build(string Key)
        {
            switch (Key)
            {
                case nameof(Category):
                    return new CategoriesServices<TRequest, TResponse>(_CategoryRepository);
                case nameof(Product):
                    return new ProductsServices<TRequest, TResponse>(_CategoryRepository, _ProductRepository);
                case nameof(Core.Entities.Inventories.Inventory):
                    return new InventoriesServices<TRequest, TResponse>(_inventoryRepository);
                case nameof(Transaction):
                    return new TransactionsServices<TRequest, TResponse>(_TransactionRepository);
                default: return null;
            }
        }
    }
}
