using SocialMediaPlatformAPI.Dtos.Comment;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int commentId);
    Task<Comment> CreateAsync(Comment commentModel);
    Task<Comment?> UpdateAsync(int commentId, Comment commentDto);
    Task<bool> DeleteAsync(int commentId);
}