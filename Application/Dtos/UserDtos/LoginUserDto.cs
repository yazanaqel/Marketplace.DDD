using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UserDtos;
public class LoginUserDto {
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}