using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatformAPI.Dtos.Comment;
using SocialMediaPlatformAPI.Extensions;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Mappers;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Controllers;

[Route("api/v1/comment")]
[ApiController]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    private readonly UserManager<AppUser> _userManager;
    public CommentController(
        ICommentRepository commentRepository,
        IStockRepository stockRepository, UserManager<AppUser> userManager)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentDto = comments.Select(CommentMappers.ToCommentDto);
        
        return Ok(commentDto);
    }

    [HttpGet("{commentId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment is null)
        {
            return NotFound();
        }

        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockExists = await  _stockRepository.StockExists(stockId);
        if (!stockExists)
        {
            return BadRequest("Stock Doesn't Exist");
        }

        var uid = User.GetUID();
        var appuser = await _userManager.FindByIdAsync(uid);
        var commentModel = commentDto.FromCreateToComment(stockId);
        commentModel.AppUserId = appuser.Id;
        commentModel.AppUser = appuser;
        await _commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(
            nameof(GetById), new{ commentId=commentModel.Id}, commentModel.ToCommentDto());
    }

    [HttpPut("{commentId:int}")]
    public async Task<IActionResult> Update([FromRoute] int commentId,[FromBody] UpdateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await _commentRepository.UpdateAsync(commentId,CommentMappers.FromUpdateToComment(commentDto));
        if (comment is null)
        {
            return NotFound("Comment Not Found");
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int commentId)
    {
        var comment = await _commentRepository.DeleteAsync(commentId);
        if (!comment)
        {
            return NotFound();
        }

        return NoContent();
    }
}