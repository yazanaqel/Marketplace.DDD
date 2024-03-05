using Application.Commands.Users.CreateUser;
using Application.Commands.Users.LoginUser;
using Application.Dtos.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto) {

        var result = await _mediator.Send(new RegisterUserCommand(dto));

        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto) {

        var result = await _mediator.Send(new LoginUserCommand(dto));

        return Ok(result);
    }
}