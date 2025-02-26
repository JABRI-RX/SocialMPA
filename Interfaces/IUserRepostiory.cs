using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Interfaces;

public interface IUserRepository
{
    Task<AppUser> FindUserById(string id);
}