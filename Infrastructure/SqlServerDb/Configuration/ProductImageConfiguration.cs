using Domain.Domain.Products;
using Domain.Entities.Images;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServerDb.Configuration;
internal sealed class ProductImageConfiguration {

    internal static void ConfigureProductImages(ModelBuilder modelBuilder) {

        modelBuilder.Entity<ProductImage>()
            .HasKey(k => k.ImageId)
            .IsClustered(false);

        modelBuilder.Entity<ProductImage>()
            .Property(id => id.ImageId)
            .HasConversion(imageId => imageId.Value, value => new ImageId(value));

        modelBuilder.Entity<ProductImage>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(f => f.ProductId);
    }
}
