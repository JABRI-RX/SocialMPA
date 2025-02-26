using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatformAPI.Dtos.Stock;

public class UpdateStockRequestDto
{
    [Required]
    [MaxLength(10,ErrorMessage = "Symbole Cannot Be Over 10 characters")]
    public string Symbole { get; set; } = string.Empty;
    [Required]
    [MaxLength(20,ErrorMessage = "CompanyName Cannot Be Over 20 characters")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1,1_000_000_000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001,100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10,ErrorMessage = "Industry cannot be over 10 characters")]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1,5_000_000_000)]
    public long MarketCap { get; set; }
}