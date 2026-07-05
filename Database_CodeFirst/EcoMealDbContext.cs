using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EcoMeal1.Database_CodeFirst
{
    public class EcoMealDbContext : IdentityDbContext<EcoMealUser>
    {
        public EcoMealDbContext(DbContextOptions<EcoMealDbContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Businesses> Businesses { get; set; }
        public DbSet<BusinessesType> BusinessesTypes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<OrderPackage> OrderPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderPackage>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderPackages)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Restrict);  

            builder.Entity<OrderPackage>()
                .HasOne(op => op.Package)
                .WithMany(p => p.OrderPackages)
                .HasForeignKey(op => op.PackageId)
                .OnDelete(DeleteBehavior.Restrict);  

            builder.Entity<Order>()
                .HasOne(o => o.Business)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BusinessId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<int>();

            builder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "Shipped" },
                new Status { Id = 3, Name = "Delivered" }
            );

            builder.Entity<Businesses>()
                .Property(b => b.BusinessTypeId)
                .HasConversion<int>();

            builder.Entity<BusinessesType>().HasData(
                new BusinessesType { Id = 1, Name = "Restaurant" },
                new BusinessesType { Id = 2, Name = "Cafe" },
                new BusinessesType { Id = 3, Name = "Bakery" },
                new BusinessesType { Id = 4, Name = "Supermarket" },
                new BusinessesType { Id = 5, Name = "Grocery Store" }
                );

            builder.Entity<Package>()
                .Property(p => p.PackageTypeId)
                .HasConversion<int>();

            builder.Entity<PackageType>().HasData(
                new PackageType { Id = 1, Name = "Surprise Bag" },
                new PackageType { Id = 2, Name = "Daily Menu" },
                new PackageType { Id = 3, Name = "Pastry" },
                new PackageType { Id = 4, Name = "Fruits and Vegetables" },
                new PackageType { Id = 5, Name = "Groceries" },
                new PackageType { Id = 6, Name = "Dairy" },
                new PackageType { Id = 7, Name = "Meat and Poultry" },
                new PackageType { Id = 8, Name = "Dessert" },
                new PackageType { Id = 9, Name = "Vegan" }
            );

        }

    }
}
