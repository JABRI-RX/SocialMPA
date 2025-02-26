using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatformAPI.Extensions;
using SocialMediaPlatformAPI.Helpers;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Controllers;

[ApiController]
[Route("api/v1/portfolio")]
[Authorize]

public class PortfolioController: ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    public PortfolioController(IStockRepository stockRepository, UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository)
    {
        _stockRepository = stockRepository;
        _userManager = userManager;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserPorfolio()
    {

        var uid = User.GetUID();
        var appUser = await _userManager.FindByIdAsync(uid);
        MyLogger.LogMe(appUser.Id);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }

    [HttpPost("{symbol}")]
    public async Task<IActionResult> AddPortfolio([FromRoute] string symbol)
    {
        MyLogger.LogMe(symbol);
        var uid = User.GetUID();
        var appUser = await _userManager.FindByIdAsync(uid);
        var stock = await _stockRepository.GetStockBySymbole(symbol);
        if (stock is null)
            return NotFound("Stock Not Found");
    
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        if (userPortfolio.Any(
                p => p.Symbole.ToLower().Equals(symbol.ToLower()))
           )
            return BadRequest("Stock Already Exists Dawg 🙂");
        var portfolio = new Portfolio
        {
            AppUserId = appUser.Id,
            StockId = stock.Id
        };
        await _portfolioRepository.CreateAsync(portfolio);
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePortfolio( string symbol)
    {
        var uid = User.GetUID();
        var appUser = await _userManager.FindByIdAsync(uid);
        var portfolios = await _portfolioRepository.GetUserPortfolio(appUser);
        var filteredStock = portfolios.Where(p => p.Symbole.ToLower().Equals(symbol.ToLower()));
        if (filteredStock.Count().Equals(0))
        {
            return BadRequest("Stock Not In your Portfolio");
        }
        await _portfolioRepository.DeleteAsync(appUser, symbol);
        return Ok("Stock Removed From Your Portfolio");
    }
  
}