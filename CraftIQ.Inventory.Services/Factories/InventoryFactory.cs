using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.CategoriesImplementations;
using huzcodes.Persistence.Interfaces.Repositories;

namespace CraftIQ.Inventory.Services.Factories
{
    public class InventoryFactory<TRequest, TResponse>(IRepository<Category> CategoryRepository)
    {
        private readonly IRepository<Category> _CategoryRepository = CategoryRepository;
        public IGenericServices<TRequest, TResponse> Build(string Key)
        {
            switch (Key)
            {
                case nameof(Category):
                    return new CategoriesServices<TRequest, TResponse>(_CategoryRepository);
                default: return null;
            }
        }
    }
}
