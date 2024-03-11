namespace Domain.Domain.Users;
public interface IUserService {
    Task<ApplicationResponse<string>> LoginAsync(ApplicationUser user);
    Task<ApplicationResponse<string>> RegisterAsync(ApplicationUser user);
}
