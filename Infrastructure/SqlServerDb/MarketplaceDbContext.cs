using Application.Application.Abstractions;
using Domain.Domain.Products;
using Domain.Domain.Users;
using Domain.Entities.Images;
using Infrastructure.SqlServer.Configuration;
using Infrastructure.SqlServerDb.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer;

public class MarketplaceDbContext : IdentityDbContext<ApplicationUser>, IDbContext, IUnitOfWork {

    public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) : base(options) {


    }
    public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        UserConfiguration.ConfigureApplicationUser(modelBuilder);
        ProductConfiguration.ConfigureProduct(modelBuilder);
        ProductImageConfiguration.ConfigureProductImages(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {

        return await base.SaveChangesAsync(cancellationToken);
    }

}
