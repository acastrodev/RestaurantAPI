using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Saal.Restaurant.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            if (isDevelopment)
            {
                services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("AWSRdsConnection");
                services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionString));
            }
        }

        public static void SeedData(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
            DataSeeder.SeedInMemory(context);
        }
    }
}