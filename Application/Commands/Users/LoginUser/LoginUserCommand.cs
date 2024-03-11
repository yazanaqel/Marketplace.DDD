using Application.Dtos.UserDtos;
using Domain;
using Domain.Constants;
using Domain.Domain.Users;
using MediatR;

namespace Application.Commands.Users.LoginUser;
public record LoginUserCommand(LoginUserDto Dto) : ICommand<ApplicationResponse<UserResponseDto>>;
public class RegisterUserHandler(IUserService userRepository) : IRequestHandler<LoginUserCommand, ApplicationResponse<UserResponseDto>> {

    private readonly IUserService _userRepository = userRepository;

    public async Task<ApplicationResponse<UserResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken) {

        var response = new ApplicationResponse<UserResponseDto>();

        var user = ApplicationUser.LoginUser(request.Dto.Email, request.Dto.Password);

        var result = await _userRepository.LoginAsync(user);

        if (result is { Success: true, Data: not null }) {

            response.Data = new UserResponseDto { JWT = result.Data };
            response.Message = CustomConstants.Operation.Successful;
            return response;
        }

        response.Message = result.Message;
        return response;
    }
}