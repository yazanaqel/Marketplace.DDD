using Application.Application.Abstractions;
using Domain.Domain.Products;
using Domain.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.SqlServer;

public class MarketplaceDbContext : IdentityDbContext<ApplicationUser>, IDbContext, IUnitOfWork {
    public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) : base(options) {

        
    }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<Product>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(p => p.UserId);

        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {

        return await base.SaveChangesAsync(cancellationToken);
    }

}
