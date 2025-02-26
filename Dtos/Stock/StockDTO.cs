using SocialMediaPlatformAPI.Dtos.Comment;

namespace SocialMediaPlatformAPI.Dtos.Stock;

public class StockDTO
{
    public int Id { get; set; }
    public string Symbole { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    public List<CommentDto> Comments { get; set; } = [];
}