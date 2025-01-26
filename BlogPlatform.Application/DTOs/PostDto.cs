using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Application.DTOs
{
    public class PostDto    
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}