using Microsoft.AspNetCore.Identity;

namespace Domain.Domain.Users;
public sealed class ApplicationUser : IdentityUser {
    private ApplicationUser() { }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ModifiedOnUtc { get; private set; }

    public static ApplicationUser CreateUser(string firstName, string lastName, string email, string username, string password) {

        var user = new ApplicationUser {

            FirstName = firstName,
            LastName = lastName,
            Email = email,
            UserName = username,
            Password = password,
            CreatedOnUtc = DateTime.UtcNow,
        };

        return user;
    }

    public static ApplicationUser LoginUser(string email, string password) {

        var user = new ApplicationUser {

            Email = email,
            Password = password,
        };

        return user;
    }
}
