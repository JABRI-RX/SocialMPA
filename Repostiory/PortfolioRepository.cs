using Microsoft.EntityFrameworkCore;
using SocialMediaPlatformAPI.Data;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Repostiory;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDbContext _context;

    public PortfolioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return  await _context.Portfolios
            .Where(p => p.AppUserId == user.Id)
            .Select(portfolio => new Stock
            {
                Id = portfolio.StockId,
                Symbole = portfolio.Stock.Symbole,
                CompanyName = portfolio.Stock.CompanyName,
                Purchase = portfolio.Stock.Purchase,
                LastDiv = portfolio.Stock.LastDiv,
                Industry = portfolio.Stock.Industry,
                MarketCap = portfolio.Stock.MarketCap,
                Comments = portfolio.Stock.Comments
            })
            .ToListAsync();
        
    }

    public async Task<List<Portfolio>> GetAllPortfoliosExist()
    {
        return await _context.Portfolios.ToListAsync();
    }

    public async Task<Portfolio> CreateAsync(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;

    }

    public async Task<bool> DeleteAsync(AppUser appUser, string symbol)
    {
        var portfolio = await _context.Portfolios
            .FirstOrDefaultAsync(
                p => p.AppUserId.Equals(appUser.Id) && p.Stock.Symbole.Equals(symbol)
                );
        if (portfolio is null)
            return false;
        _context.Portfolios.Remove(portfolio);
        await _context.SaveChangesAsync();
        return true;
    }
}