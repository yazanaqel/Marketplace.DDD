using Application.Commands.Products.CreateProduct;
using Application.Dtos.ProductDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController(IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto) {

        var result = await _mediator.Send(new CreateProductCommand(dto));

        return Ok(result);
    }
}
