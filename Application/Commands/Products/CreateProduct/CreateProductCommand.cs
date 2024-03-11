using Application.Application.Extensions;
using Application.Dtos.ProductDtos;
using AutoMapper;
using Domain;
using Domain.Constants;
using Domain.Domain.Products;
using Domain.Entities.Products;
using MediatR;

namespace Application.Commands.Products.CreateProduct;
public record CreateProductCommand(CreateProductDto Dto) : BaseRequest, ICommand<ApplicationResponse<ProductResponseDto>>;

public class CreateProductHandler(IProductService productRepository, IMapper mapper) : IRequestHandler<CreateProductCommand, ApplicationResponse<ProductResponseDto>> {
    private readonly IProductService _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ApplicationResponse<ProductResponseDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken) {

        var response = new ApplicationResponse<ProductResponseDto>();

        var product = Product.CreateProduct(
            request.Dto.ProductName,
            new Money(request.Dto.Currency, request.Dto.Amount),
            request.Dto.Description,
            request.UserId,
            request.Dto.Images);

        var result = await _productRepository.CreateProduct(product);

        if (result is { Success: true, Data: not null }) {

            response.Data = _mapper.Map<ProductResponseDto>(result.Data);
            response.Message = CustomConstants.Operation.Successful;

            return response;
        }

        response.Message = result.Message;
        return response;
    }

}