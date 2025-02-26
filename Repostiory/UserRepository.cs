using Microsoft.AspNetCore.Identity;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Repostiory;

public class UserRepository: IUserRepository
{
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public Task<AppUser> FindUserById(string id)
    {
        throw new NotImplementedException();
    }
}