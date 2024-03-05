namespace Domain.Domain.Users;
public interface IUserRepository {
    Task<DomainResponse<string>> LoginAsync(ApplicationUser user);
    Task<DomainResponse<string>> RegisterAsync(ApplicationUser user);
}
