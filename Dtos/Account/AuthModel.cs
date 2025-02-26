namespace SocialMediaPlatformAPI.Dtos.Account;

public class AuthModel
{
    public string Message { get; set; }
    public string Token { get; set; }
    public bool IsAuthenticated { get; set; }
    public DateTime ExpiresOn { get; set; }
}