using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.OrderDetails;
using CraftIQ.Inventory.Core.Entities.Orders;
using CraftIQ.Inventory.Core.Entities.Products;
using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.CategoriesImplementations;
using CraftIQ.Inventory.Services.InventoriesImplementations;
using CraftIQ.Inventory.Services.OrderDetailsImplementaions;
using CraftIQ.Inventory.Services.OrdersImplementations;
using CraftIQ.Inventory.Services.ProductsImplementations;
using CraftIQ.Inventory.Services.TransactionsImplementaions;
using huzcodes.Persistence.Interfaces.Repositories;

namespace CraftIQ.Inventory.Services.Factories
{
    public class InventoryFactory<TRequest, TResponse>(IRepository<Category> CategoryRepository
        , IRepository<Product> ProductRepository, IRepository<Core.Entities.Inventories.Inventory> InventoryRepository,
        IRepository<Transaction> TransactionRepository, IRepository<Order> OrderRepository,
        IRepository<OrderDetail> OrderDetailsRepository)
    {
        private readonly IRepository<Category> _CategoryRepository = CategoryRepository;
        private readonly IRepository<Product> _ProductRepository = ProductRepository;
        private readonly IRepository<Core.Entities.Inventories.Inventory> _InventoryRepository = InventoryRepository;
        private readonly IRepository<Transaction> _TransactionRepository = TransactionRepository;
        private readonly IRepository<Order> _OrderRepository = OrderRepository;
        private readonly IRepository<OrderDetail> _OrderDetailsRepository = OrderDetailsRepository;
        public IGenericServices<TRequest, TResponse> Build(string Key)
        {
            switch (Key)
            {
                case nameof(Category):
                    return new CategoriesServices<TRequest, TResponse>(_CategoryRepository);
                case nameof(Product):
                    return new ProductsServices<TRequest, TResponse>(_CategoryRepository, _ProductRepository, _InventoryRepository, _TransactionRepository);
                case nameof(Core.Entities.Inventories.Inventory):
                    return new InventoriesServices<TRequest, TResponse>(_InventoryRepository);
                case nameof(Transaction):
                    return new TransactionsServices<TRequest, TResponse>(_TransactionRepository);
                case nameof(Order):
                    return new OrdersServices<TRequest, TResponse>(_OrderRepository);
                case nameof(OrderDetail):
                    return new OrderDetailsServices<TRequest, TResponse>(_OrderRepository, _ProductRepository, _OrderDetailsRepository);
                default: return null;
            }
        }
    }
}
