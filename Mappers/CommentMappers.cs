using SocialMediaPlatformAPI.Dtos.Comment;
using SocialMediaPlatformAPI.Helpers;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
  
        return new CommentDto
        {
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId,
            CreatedBy =  commentModel.AppUser.UserName ?? "check CommentMapper.cs",
        };
    }

    public static Comment FromCreateToComment(this  CreateCommentDto createCommentDto,int stockId)
    {
        return new Comment
        {
            Title = createCommentDto.Title,
            Content = createCommentDto.Content,
            StockId = stockId
        };
    }

    public static Comment FromUpdateToComment(UpdateCommentDto updateCommentDto)
    {
        return new Comment
        {
            Title = updateCommentDto.Title,
            Content = updateCommentDto.Content,
        };
    }
}