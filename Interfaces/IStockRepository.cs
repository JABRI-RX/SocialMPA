using SocialMediaPlatformAPI.Dtos.Comment;
using SocialMediaPlatformAPI.Dtos.Stock;
using SocialMediaPlatformAPI.Helpers;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetStockByIdAsync(int stockId);
    Task<Stock?> GetStockBySymbole(string symbol);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id,UpdateStockRequestDto updateStockDto);
    Task<bool> DeleteAsync(int stockId);
    Task<bool> StockExists(int stockId);
}