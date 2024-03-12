using Application.Application.Abstractions;
using Application.Application.Extensions;
using Application.Dtos.ProductDtos;
using AutoMapper;
using Domain;
using Domain.Constants;
using Domain.Domain.Products;
using Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Queries.Products.GetProductDetails;
public record GetProductDetailsQuery(ProductId ProductId)
    : IRequest<ApplicationResponse<ProductResponseDto>>;

public class GetProductDetailsHandler(IDistributedCache cache, IDbContext dbContext, IMapper mapper) : IRequestHandler<GetProductDetailsQuery, ApplicationResponse<ProductResponseDto>> {
    private readonly IDistributedCache _cache = cache;
    private readonly IDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ApplicationResponse<ProductResponseDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken) {

        var response = new ApplicationResponse<ProductResponseDto>();

        string recordKey = "product" + DateTime.Now.ToString("yyyyMMdd_hhmm");

        var cache = await _cache.GetRecordAsync<ApplicationResponse<ProductResponseDto>>(recordKey);

        if (cache is not null) {

            response.Message = cache.Message;
            response.Success = cache.Success;
            response.PageInfo = cache.PageInfo;
            response.Data = _mapper.Map<ProductResponseDto>(cache.Data);
            return response;
        }

        var result = await GetProductDetails(request.ProductId);

        if (result is { Success: true, Data: not null }) {


            response.Message = result.Message;
            response.Success = result.Success;
            response.PageInfo = result.PageInfo;
            response.Data = _mapper.Map<ProductResponseDto>(result.Data);

            await _cache.SetRecordAsync(recordKey, response);
            return response;
        }

        response.Message = result.Message;
        response.Success = result.Success;
        return response;
    }

    public async Task<ApplicationResponse<Product>> GetProductDetails(ProductId productId) {

        var response = new ApplicationResponse<Product>();

        try {


            var product = await _dbContext.Products
            .Where(product => product.ProductId == productId)
            .AsSplitQuery()
            //.Include(attribute => attribute.ProductAttributes.OrderBy(attribute => attribute.AttributeName))
            //.ThenInclude(variant => variant.ProductVariants)
            .AsNoTracking()
            .FirstOrDefaultAsync();

            if (product is null) {

                response.Message = CustomConstants.NotFound.NoProducts;
                response.Success = true;
                return response;
            }

            response.Data = product;

        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
        }


        response.Message = CustomConstants.Operation.Successful;
        response.Success = true;

        return response;
    }

}