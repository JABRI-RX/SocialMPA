namespace SocialMediaPlatformAPI.Helpers.Auths;
using Microsoft.AspNetCore.Mvc;

public class HelperUser
{
    public static string GetCurrentUserId()
    {
        // User.Claims.FirstOrDefault(u => u.Type == "uid").Value;
        return "";
    }
}