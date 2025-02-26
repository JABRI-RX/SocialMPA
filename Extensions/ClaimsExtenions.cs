using System.Security.Claims;

namespace SocialMediaPlatformAPI.Extensions;

public static class ClaimsExtenions
{
    public static string GetUID(this ClaimsPrincipal user)
    {
        return user.Claims.SingleOrDefault(c => c.Type.Equals("uid")).Value;
    }
}