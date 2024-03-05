namespace Application.Dtos.ProductDtos;
public class ProductResponseDto {
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ProductMainImage { get; set; }
    public string? Description { get; set; }
    public List<string>? ProductImages { get; set; }
}
