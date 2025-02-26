using SocialMediaPlatformAPI.Dtos.Stock;
using SocialMediaPlatformAPI.Helpers;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Mappers;

public static class StockMappers
{
    public static StockDTO ToStockDto(this Stock stock)
    {
        return new StockDTO
        {
            Id = stock.Id,
            Symbole = stock.Symbole,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap,
            Comments = stock.Comments.Select(c=>c.ToCommentDto()).ToList()
            // Comments = []
        };
        
    }

    public static Stock FromCreateDtoToStock(this  CreatedStockRequestDto stockRequestDto)
    {
        return new Stock
        {
            Symbole = stockRequestDto.Symbole,
            CompanyName = stockRequestDto.CompanyName,
            Purchase = stockRequestDto.Purchase,
            LastDiv = stockRequestDto.LastDiv,
            Industry = stockRequestDto.Industry,
            MarketCap = stockRequestDto.MarketCap,
 
        };
    }
}