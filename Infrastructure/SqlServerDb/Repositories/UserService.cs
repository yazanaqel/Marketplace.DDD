using Domain;
using Domain.Constants;
using Domain.Domain.Users;
using Infrastructure.SqlServerDb.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.SqlServer.Repositories;
internal class UserService(UserManager<ApplicationUser> userManager, IOptions<HelperJWT> helperJWT) : IUserService {

    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly HelperJWT _helperJWT = helperJWT.Value;
    public async Task<ApplicationResponse<string>> LoginAsync(ApplicationUser user) {

        var response = new ApplicationResponse<string>();

        try {

            var userFind = await _userManager.FindByEmailAsync(user.Email);

            if (userFind is null || !await _userManager.CheckPasswordAsync(userFind, user.Password)) {

                response.Message = CustomConstants.User.Incorrect;
                return response;
            }

            var jwtToken = await CreateJwtToken(userFind);
            string stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            response.Data = stringToken;
        }
        catch (Exception e) {

            Console.WriteLine(e.Message);
        }

        response.Success = true;
        return response;
    }
    public async Task<ApplicationResponse<string>> RegisterAsync(ApplicationUser user) {

        var response = new ApplicationResponse<string>();

        try {

            if (await _userManager.FindByEmailAsync(user.Email) is not null) {

                response.Message = CustomConstants.User.EmailIsTaken;
                return response;
            }

            var result = await _userManager.CreateAsync(user, user.Password);

            if (!result.Succeeded) {
                var errors = string.Empty;

                foreach (var error in result.Errors) {
                    errors += $"{error.Description},";
                }

                response.Message = errors;
                return response;
            }

            var jwtToken = await CreateJwtToken(user);
            string stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            response.Data = stringToken;
        }
        catch (Exception e) {

            Console.WriteLine(e.Message);
        }

        response.Success = true;
        return response;
    }
    private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user) {
        try {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            }
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_helperJWT.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _helperJWT.Issuer,
                audience: _helperJWT.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_helperJWT.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }
    }

}
