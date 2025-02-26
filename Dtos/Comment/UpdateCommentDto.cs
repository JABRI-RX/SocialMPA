using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatformAPI.Dtos.Comment;

public class UpdateCommentDto
{
    [Required]
    [MinLength(5,ErrorMessage = "Title Must Be 5 charters long ")]
    [MaxLength(200,ErrorMessage = "Title Cannot Be Over 200 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5,ErrorMessage = "Title Must Be 5 charters long ")]
    [MaxLength(200,ErrorMessage = "Title Cannot Be Over 200 characters")]
    public string Content { get; set; } = string.Empty;
}