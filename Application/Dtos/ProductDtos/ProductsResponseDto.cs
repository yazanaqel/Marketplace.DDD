using Domain.Domain.Products;
using Domain.Entities.Products;

namespace Application.Dtos.ProductDtos;
public class ProductsResponseDto {
    public ProductId ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductMainImage { get; set; }
    public Money Price { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
}
