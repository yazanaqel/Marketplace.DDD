using Application.Dtos.UserDtos;
using Domain;
using Domain.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Users.LoginUser;
public record LoginUserCommand(LoginUserDto Dto) : ICommand<ApplicationResponse<UserResponseDto>>;
public class RegisterUserHandler(IUserRepository userRepository) : IRequestHandler<LoginUserCommand, ApplicationResponse<UserResponseDto>> {

    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ApplicationResponse<UserResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken) {

        var response = new ApplicationResponse<UserResponseDto>();

        var user = new ApplicationUser(request.Dto.Email, request.Dto.Password);

        var result = await _userRepository.LoginAsync(user);

        if (result is { Success: true, Data: not null }) {

            response.Data = new UserResponseDto { JWT = result.Data };
            response.StatusCode = StatusCodes.Status200OK;
            return response;
        }

        response.Message = result.Message;
        return response;
    }
}