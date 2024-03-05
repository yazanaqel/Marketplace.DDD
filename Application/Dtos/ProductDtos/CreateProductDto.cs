using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ProductDtos;
public class CreateProductDto {

    [Required, MaxLength(15)]
    public required string ProductName { get; set; }

    [Required]
    public required decimal Price { get; set; }
    [MaxLength(50)]
    public string? Description { get; set; }

}
