using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolio(AppUser user);
    Task<List<Portfolio>> GetAllPortfoliosExist();
    Task<Portfolio> CreateAsync(Portfolio portfolio);
    Task<bool> DeleteAsync(AppUser appUser, string symbol);
}