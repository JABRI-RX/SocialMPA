using System.Security.Claims;

namespace SocialMediaPlatformAPI.Helpers;

public static class ClaimsHelpers
{
    public static string GetId(ClaimsPrincipal user)
    {
        return user.Claims.SingleOrDefault(c => c.Type.Equals("uid")).Value;
    }
}