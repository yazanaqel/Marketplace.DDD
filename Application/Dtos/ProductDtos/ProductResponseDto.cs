﻿using Domain.Domain.Products;
using Domain.Entities.Products;

namespace Application.Dtos.ProductDtos;
public class ProductResponseDto {
    public ProductId ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductMainImage { get; set; }
    public Money Price { get; set; }
    public string? Description { get; set; }
    public List<string>? ProductImages { get; set; }
}
