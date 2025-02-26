using Microsoft.AspNetCore.Identity;

namespace SocialMediaPlatformAPI.Models;

public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = [];
}