using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Domain;

namespace Saal.Restaurant.Infrastructure
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>().HasData(CreateMenuItems());
            modelBuilder.Entity<Table>().HasData(CreateTables());
        }

        public static void SeedInMemory(RestaurantDbContext context)
        {
            if (!context.MenuItems.Any())
            {
                context.MenuItems.AddRange(CreateMenuItems());
            }

            if (!context.Tables.Any())
            {
                context.Tables.AddRange(CreateTables());
            }

            context.SaveChanges();
        }

        private static List<Table> CreateTables()
        {
            return
                [
                    new Table { Id = 1, Description = "Table number 1" },
                    new Table { Id = 2, Description = "Table number 2" },
                    new Table { Id = 3, Description = "Table number 3" }
                ];
        }

        private static List<MenuItem> CreateMenuItems()
        {
            return
                [
                    new MenuItem { Id = 1, Description = "Cheeseburger", Price = 5.99m },
                    new MenuItem { Id = 2, Description = "Pizza Margherita", Price = 8.99m },
                    new MenuItem { Id = 3, Description = "Pasta Carbonara", Price = 7.99m },
                    new MenuItem { Id = 4, Description = "Coca-Cola", Price = 2.99m}
                ];
        }
    }
}