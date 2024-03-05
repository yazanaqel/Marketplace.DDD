using Microsoft.AspNetCore.Identity;

namespace Domain.Domain.Users;
public sealed class ApplicationUser : IdentityUser {

    public ApplicationUser(string firstName, string lastName, string email, string username, string password) {

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = username;
        Password = password;
    }
    public ApplicationUser(string email, string password) {

        Email = email;
        Password = password;
    }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public DateTime CreatedOnUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? ModifiedOnUtc { get; private set; }

}
