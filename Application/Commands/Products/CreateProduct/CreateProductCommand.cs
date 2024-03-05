using Application.Application.Extensions;
using Application.Dtos.ProductDtos;
using Domain;
using Domain.Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Products.CreateProduct;
public record CreateProductCommand(CreateProductDto Dto) : BaseRequest, ICommand<ApplicationResponse<ProductResponseDto>>;

public class CreateProductHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, ApplicationResponse<ProductResponseDto>> {
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ApplicationResponse<ProductResponseDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken) {

        var response = new ApplicationResponse<ProductResponseDto>();

        var product = new Product(
            request.Dto.ProductName,
            null,
            request.Dto.Price,
            request.Dto.Description,
            request.UserId);

        var result = await _productRepository.CreateProduct(product);

        if (result is { Success: true, Data: not null }) {

            response.Data = new ProductResponseDto {ProductName=result.Data.ProductName  };
            response.StatusCode = StatusCodes.Status201Created;
            return response;
        }

        response.Message = result.Message;
        return response;
    }

}