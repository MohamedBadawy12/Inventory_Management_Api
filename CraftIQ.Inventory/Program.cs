using CraftIQ.Inventory.Infrastructure;
using CraftIQ.Inventory.Services;
using huzcodes.Extensions.Exceptions;
namespace CraftIQ.Inventory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //adding db context registeration
            var inventoryDbConnectionString = builder.Configuration.GetSection("ConnectionStrings:InventoryConnectionString");
            builder.Services.AddInventoryDbContext(inventoryDbConnectionString.Value!);
            builder.Services.AddInfrastructureRegistrations();
            builder.Services.AddServicesRegistrations();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.AddExceptionHandlerExtension();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
