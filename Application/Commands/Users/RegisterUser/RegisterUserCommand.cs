using Application.Dtos.UserDtos;
using Domain;
using Domain.Constants;
using Domain.Domain.Users;
using MediatR;

namespace Application.Commands.Users.CreateUser;
public record RegisterUserCommand(RegisterUserDto Dto) : ICommand<ApplicationResponse<UserResponseDto>>;
public class RegisterUserHandler(IUserService userRepository) : IRequestHandler<RegisterUserCommand, ApplicationResponse<UserResponseDto>> {

    private readonly IUserService _userRepository = userRepository;

    public async Task<ApplicationResponse<UserResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken) {

        var response = new ApplicationResponse<UserResponseDto>();

        var user = ApplicationUser.CreateUser(
            request.Dto.FirstName,
            request.Dto.LastName!,
            request.Dto.Email,
            request.Dto.Email,
            request.Dto.Password);

        var result = await _userRepository.RegisterAsync(user);

        if (result is { Success: true, Data: not null }) {

            response.Data = new UserResponseDto { JWT = result.Data };
            response.Message = CustomConstants.Operation.Successful;
            return response;
        }

        response.Message = result.Message;
        return response;
    }
}