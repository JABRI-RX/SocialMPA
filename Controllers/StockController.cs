using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatformAPI.Dtos.Stock;
using SocialMediaPlatformAPI.Helpers;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Mappers;


namespace SocialMediaPlatformAPI.Controllers;
[Route("api/v1/stock")]
[ApiController]
[Authorize]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;
    public StockController(
        IStockRepository stockRepository
        )
    {
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public  async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        
        var stocks = await _stockRepository.GetAllAsync(query);
        var stockDto = stocks.Select(StockMappers.ToStockDto).ToList();
        return Ok(stockDto);
        
    }

    [HttpGet("{stockId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int stockId)
    {
 
        var stock = await _stockRepository.GetStockByIdAsync(stockId);
        if (stock is null)
        {
            return NotFound();
        }
         
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatedStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockModel = stockDto.FromCreateDtoToStock();
        await _stockRepository.CreateAsync(stockModel);
        
        return CreatedAtAction(
            nameof(GetById),
            new { stockId = stockModel.Id },
            stockModel.ToStockDto()
            );
    }

    [HttpPut("{stockId:int}")]
    public async Task<IActionResult> Update([FromRoute] int stockId, [FromBody] UpdateStockRequestDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockModel = await _stockRepository.UpdateAsync(stockId, updateDto);
        if (stockModel is null) {
            return NotFound();
        }
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{stockId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int stockId)
    {
        var stockModel = await _stockRepository.DeleteAsync(stockId);
        if (!stockModel)
        {
            return NotFound();
        }

        return NoContent();
    }
}     
