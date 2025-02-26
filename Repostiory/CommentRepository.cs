using Microsoft.EntityFrameworkCore;
using SocialMediaPlatformAPI.Data;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Repostiory;

public class CommentRepository: ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments
            .Include(c=> c.AppUser)
            .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int commentId)
    {
        return await _context.Comments
            .Include(c => c.AppUser)
            .FirstOrDefaultAsync(c => c.Id.Equals(commentId));
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int commentId, Comment commentDto)
    {
        var commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (commentModel is null)
        {
            return null;
        }
        commentModel.Title = commentDto.Title;
        commentModel.Content = commentDto.Content;
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<bool> DeleteAsync(int commentId)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment is null)
        {
            return false;
        }

        _context.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }
}