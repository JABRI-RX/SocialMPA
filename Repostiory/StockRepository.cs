using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaPlatformAPI.Data;
using SocialMediaPlatformAPI.Dtos.Stock;
using SocialMediaPlatformAPI.Helpers;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Mappers;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Repostiory;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _context;
    public StockRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
     
        var stocks = _context.Stocks
            .Include(c=>c.Comments)
            .ThenInclude(c=>c.AppUser)
            .AsQueryable();
        if (!string.IsNullOrEmpty(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrEmpty(query.Symbole))
        {
            stocks = stocks.Where(s => s.Symbole.Contains(query.Symbole));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbole", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDescending ? 
                    stocks.OrderByDescending(s => s.Symbole) : 
                    stocks.OrderBy(s => s.Symbole);
            }
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        return await stocks
            .Skip(skipNumber)
            .Take(query.PageNumber)
            .ToListAsync();
    }

    public async Task<Stock?> GetStockByIdAsync(int stockId)
    {
        var stocks = await _context.Stocks
            .Include(c=>c.Comments)
            .ThenInclude(c=>c.AppUser)
            .FirstOrDefaultAsync(s => s.Id.Equals(stockId));
        
        // return await _context.Stocks
            // .Include(c=>c.Comments)
            // .FirstOrDefaultAsync(s=>s.Id == stockId);
            return stocks;
    }

    public async Task<Stock?> GetStockBySymbole(string symbol)
    {
        return await _context.Stocks.
            FirstOrDefaultAsync(s => s.Symbole.Equals(symbol));
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id,UpdateStockRequestDto updateStockDto)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        if (stockModel is null)
            return null;
        stockModel.Symbole = updateStockDto.Symbole;
        stockModel.CompanyName = updateStockDto.CompanyName;
        stockModel.Purchase = updateStockDto.Purchase;
        stockModel.LastDiv = updateStockDto.LastDiv;
        stockModel.Industry = updateStockDto.Industry;
        stockModel.MarketCap = updateStockDto.MarketCap;
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<bool> DeleteAsync(int stockId)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(s=> s.Id == stockId);
        if (stockModel is null)
            return false;
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> StockExists(int stockId)
    {
        return await _context.Stocks.AnyAsync(s => s.Id == stockId);
    }
}