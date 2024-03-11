using Domain.Entities.Products;
using Microsoft.AspNetCore.Http;

namespace Domain.Domain.Products;
public class Product {
    private Product() { }
    public ProductId ProductId { get; private set; } = ProductId.Empty;
    public string ProductName { get; private set; } = string.Empty;
    public string? ProductMainImage { get; private set; }
    public Money? Price { get; private set; }
    public string? Description { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public IFormFile[]? Images { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ModifiedOnUtc { get; private set; }
    public static Product CreateProduct(

        string productName,
        Money price,
        string? description,
        string userId,
        IFormFile[]? images,
        string? productMainImage = default) {

        var product = new Product {

            ProductId = ProductId.NewProductId,
            ProductName = productName,
            ProductMainImage = productMainImage,
            Price = price,
            Description = description,
            UserId = userId,
            Images = images,
            CreatedOnUtc = DateTime.UtcNow,
        };

        return product;
    }
}
