using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UserDtos;
public class RegisterUserDto {

    [Required, MaxLength(15)]
    public required string FirstName { get; set; }

    [MaxLength(15)]
    public string? LastName { get; set; }

    [Required, EmailAddress]
    public required string Email { get; set; }

    [Required, PasswordPropertyText]
    public required string Password { get; set; }

    [Required, PasswordPropertyText, Compare(nameof(Password))]
    public string PasswordConfirm { get; set; } = string.Empty;

}
