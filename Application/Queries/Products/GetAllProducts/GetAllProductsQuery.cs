using Application.Application.Abstractions;
using Application.Dtos.ProductDtos;
using AutoMapper;
using Domain;
using Domain.Constants;
using Domain.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Queries.Products.GetAllProducts;
public record GetAllProductsQuery(string? sortColumn, string? sortOrder, string? searchItem, int page, int pageSize)
    : IRequest<ApplicationResponse<IReadOnlyList<ProductsResponseDto>>>;
public class GetAllProductsHandler(IDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, ApplicationResponse<IReadOnlyList<ProductsResponseDto>>> {
    private readonly IDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ApplicationResponse<IReadOnlyList<ProductsResponseDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) {

        var response = new ApplicationResponse<IReadOnlyList<ProductsResponseDto>>();

        var result = await GetAllProducts
            (request.sortColumn, request.sortOrder, request.searchItem, request.page, request.pageSize);

        if (result is { Success: true, Data: not null }) {


            response.Message = result.Message;
            response.Success = result.Success;
            response.PageInfo = result.PageInfo;
            response.Data = result.Data.Select(_mapper.Map<ProductsResponseDto>).ToList();

            return response;
        }


        response.Message = result.Message;
        response.Success = result.Success;
        return response;
    }

    private async Task<ApplicationResponse<IReadOnlyList<Product>>> GetAllProducts(string? sortColumn, string? sortOrder, string? searchItem, int page, int pageSize) {

        ApplicationResponse<IReadOnlyList<Product>> response = new();

        try {

            IQueryable<Product> productsQuery = _dbContext.Products.AsNoTracking();

            if (!productsQuery.Any()) {

                response.Message = CustomConstants.NotFound.NoProducts;
                response.Success = true;
                return response;
            }

            if (!string.IsNullOrWhiteSpace(searchItem)) {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(searchItem) || p.Description.Contains(searchItem));
            }

            if (sortOrder?.ToLower() == CustomConstants.SortOrder.Descinding.ToLower()) {
                productsQuery = productsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }
            else {
                productsQuery = productsQuery.OrderBy(GetSortProperty(sortColumn));
            }

            int totalCount = await productsQuery.CountAsync();

            var products = await productsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            response.Data = products.AsReadOnly();
            response.PageInfo = new PageInfo(page, pageSize, totalCount);
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
        }


        response.Message = CustomConstants.Operation.Successful;
        response.Success = true;

        return response;
    }
    private static Expression<Func<Product, object>> GetSortProperty(string? sortColumn) {
        return sortColumn?.ToLower() switch {
            "name" => product => product.ProductName,
            "price" => product => product.Price,
            _ => product => product.ProductId
        };
    }

}