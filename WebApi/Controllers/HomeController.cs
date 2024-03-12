using Application.Dtos.ProductDtos;
using Application.Queries.Products.GetAllProducts;
using Application.Queries.Products.GetProductDetails;
using Domain;
using Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HomeController(IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<ApplicationResponse<IReadOnlyList<ProductsResponseDto>>>> GetAllProducts(string? sortColumn, string? sortOrder, string? searchItem, int page = 1, int pageSize = 5) {

        var response = await _mediator.Send(new GetAllProductsQuery(sortColumn, sortOrder, searchItem, page, pageSize));

        return response;
    }

    [HttpGet("GetProductDetails")]
    public async Task<ActionResult<ApplicationResponse<ProductResponseDto>>> GetProductDetails([Required] Guid productId) {

        var response = await _mediator.Send(new GetProductDetailsQuery(new ProductId(productId)));

        return response;
    }
}
