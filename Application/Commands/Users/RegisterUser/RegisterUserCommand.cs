using Application.Dtos.UserDtos;
using Domain;
using Domain.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Users.CreateUser;
public record RegisterUserCommand(RegisterUserDto Dto) : ICommand<ApplicationResponse<UserResponseDto>>;
public class RegisterUserHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, ApplicationResponse<UserResponseDto>> {

    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ApplicationResponse<UserResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken) {

        var response = new ApplicationResponse<UserResponseDto>();

        var user = new ApplicationUser(
            request.Dto.FirstName,
            request.Dto.LastName!,
            request.Dto.Email,
            request.Dto.Email,
            request.Dto.Password);

        var result = await _userRepository.RegisterAsync(user);

        if (result is { Success: true, Data: not null }) {

            response.Data = new UserResponseDto { JWT = result.Data };
            response.StatusCode = StatusCodes.Status201Created;
            return response;
        }

        response.Message = result.Message;
        return response;
    }
}