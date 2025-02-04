using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Domain;

namespace Saal.Restaurant.Infrastructure
{
    public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : DbContext(options)
    {
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenuItem> OrderMenuItems { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureRelationships(modelBuilder);
            DataSeeder.Seed(modelBuilder);
        }

        private static void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bill>()
                .HasOne<Table>()
                .WithOne(t => t.Bill)
                .HasForeignKey<Bill>(b => b.TableId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderMenuItem>()
                .HasKey(omi => new { omi.OrderId, omi.MenuItemId });

            modelBuilder.Entity<OrderMenuItem>()
                .HasOne(omi => omi.Order)
                .WithMany(o => o.OrderMenuItems)
                .HasForeignKey(omi => omi.OrderId);

            modelBuilder.Entity<OrderMenuItem>()
                .HasOne(omi => omi.MenuItem)
                .WithMany()
                .HasForeignKey(omi => omi.MenuItemId);
        }
    }
}