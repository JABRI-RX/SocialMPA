using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaPlatformAPI.Dtos.Account;
using SocialMediaPlatformAPI.Helpers;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Repostiory;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JWT _jwt;
    public AuthRepository(
        SignInManager<AppUser> signInManager, 
        RoleManager<IdentityRole> roleManager,
        UserManager<AppUser> userManager,
        IOptions<JWT> jwt
        )
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _jwt = jwt.Value;
    }
    public async Task<AuthModel> RegisterUserAsync(RegisterDto registerDto)
    {
        if (await _userManager.FindByEmailAsync(registerDto.Email) is not null)
            return new AuthModel { Message = "Email is Already Registered" };
        
        var user = new AppUser
        {
            UserName = $"{registerDto.Username}{_userManager.Users.Count()}",
            Email = registerDto.Email,
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + "\n";
            }

            return new AuthModel { Message = errors };
        }

        await _userManager.AddToRoleAsync(user, "User");
        var jwtSecurityToken = await CreateJwtToken(user);
        return new AuthModel
        {
            ExpiresOn = jwtSecurityToken.ValidTo,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            IsAuthenticated = true
        };
    }

    public async Task<AuthModel> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null ||
            !await _userManager.CheckPasswordAsync(user,loginDto.Password)
           )
        {
            return new AuthModel { Message = "Email Or Password is incorrect" };
        }

        var jwtToken = await CreateJwtToken(user);
        return new AuthModel
        {
            IsAuthenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            ExpiresOn = jwtToken.ValidTo
        };
    }
    public async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username",user.UserName ??"Not Logged In"),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey));
        var singingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer:_jwt.Issuer,
            audience:_jwt.Audience,
            claims:claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: singingCredentials
        );
        return jwtSecurityToken;
    }

    public async Task<string> AddRoleAsync(AddRole addRole)
    {
        var appUser = await _userManager.FindByIdAsync(addRole.UserId);
        if (appUser is null || !await _roleManager.RoleExistsAsync(addRole.Role))
            return "User/ role Not Found";
        if (await _userManager.IsInRoleAsync(appUser, addRole.Role))
            return "User Already Assigned this Role";
        var result = await _userManager.AddToRoleAsync(appUser, addRole.Role);
        return result.Succeeded ? string.Empty:$"Errors {result.Errors}";
    }
}