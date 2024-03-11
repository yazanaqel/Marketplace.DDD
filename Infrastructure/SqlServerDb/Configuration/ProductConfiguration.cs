using Domain.Domain.Products;
using Domain.Domain.Users;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer.Configuration;
internal sealed class ProductConfiguration {

    internal static void ConfigureProduct(ModelBuilder modelBuilder) {


        modelBuilder.Entity<Product>()
            .HasKey(k => k.ProductId)
            .IsClustered(false);

        modelBuilder.Entity<Product>()
            .Property(id => id.ProductId)
            .HasConversion(productId => productId.Value, value => new ProductId(value));

        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Price, priceBuilder => {
                priceBuilder.Property(c => c.Currency).HasMaxLength(3);
            });

        modelBuilder.Entity<Product>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<Product>()
            .Ignore(i => i.Images);
    }
}
