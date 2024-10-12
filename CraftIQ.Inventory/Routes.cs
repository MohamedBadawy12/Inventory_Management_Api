namespace CraftIQ.Inventory
{
    public static class Routes
    {
        public class CategoriesRoutes
        {
            public const string BaseUrl = "/category";
            public const string ReadById = BaseUrl + "/{categoryId}";
            public const string Delete = BaseUrl + "/{categoryId}";
            public const string Updtate = BaseUrl + "/{categoryId}";
        }
    }
}
