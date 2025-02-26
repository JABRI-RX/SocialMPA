namespace SocialMediaPlatformAPI.Dtos.Account;

public class JWT
{
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInDays { get; set; }
}