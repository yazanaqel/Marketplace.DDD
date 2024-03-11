using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ProductDtos;
public class CreateProductDto {

    [Required, MaxLength(15)]
    public required string ProductName { get; set; }

    [Required,MaxLength(3)]
    public string Currency { get; set; } = string.Empty;

    [Required]
    public decimal Amount { get; set; }

    [MaxLength(50)]
    public string? Description { get; set; }
    public IFormFile[]? Images { get; set; }

}
