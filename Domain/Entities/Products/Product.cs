using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Products;
public class Product {
    public Product(string productName, string? productMainImage, decimal price, string? description, string userId) {

        ProductName = productName;
        ProductMainImage = productMainImage;
        Price = price;
        Description = description;
        UserId = userId;
    }
    public Guid ProductId { get; private set; } = new Guid();
    public string ProductName { get; private set; } = string.Empty;
    public string? ProductMainImage { get; private set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; private set; }
    public string? Description { get; private set; }
    public string UserId { get; private set; }

}