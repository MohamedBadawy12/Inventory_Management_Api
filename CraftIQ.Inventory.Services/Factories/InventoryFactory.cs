using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.Products;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.CategoriesImplementations;
using CraftIQ.Inventory.Services.ProductsImplementations;
using huzcodes.Persistence.Interfaces.Repositories;

namespace CraftIQ.Inventory.Services.Factories
{
    public class InventoryFactory<TRequest, TResponse>(IRepository<Category> CategoryRepository
        , IRepository<Product> ProductRepository)
    {
        private readonly IRepository<Category> _CategoryRepository = CategoryRepository;
        private readonly IRepository<Product> _ProductRepository = ProductRepository;
        public IGenericServices<TRequest, TResponse> Build(string Key)
        {
            switch (Key)
            {
                case nameof(Category):
                    return new CategoriesServices<TRequest, TResponse>(_CategoryRepository);
                case nameof(Product):
                    return new ProductsServices<TRequest, TResponse>(_CategoryRepository, _ProductRepository);
                default: return null;
            }
        }
    }
}
