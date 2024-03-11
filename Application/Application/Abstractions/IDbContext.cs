using Domain.Domain.Products;
using Domain.Domain.Users;
using Domain.Entities.Images;
using Microsoft.EntityFrameworkCore;

namespace Application.Application.Abstractions;
public interface IDbContext {
    DbSet<ApplicationUser> ApplicationUser { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductImage> ProductImages { get; set; }
}
