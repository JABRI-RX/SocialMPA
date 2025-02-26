using System.IdentityModel.Tokens.Jwt;
using SocialMediaPlatformAPI.Dtos.Account;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Interfaces;

public interface IAuthRepository
{
    Task<AuthModel> RegisterUserAsync(RegisterDto registerDto);
    Task<AuthModel> LoginUserAsync(LoginDto loginDto);
    Task<JwtSecurityToken> CreateJwtToken(AppUser user);
    Task<string> AddRoleAsync(AddRole addRole);

}