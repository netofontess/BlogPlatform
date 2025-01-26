using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllPosts();
        Task<BlogPost?> GetPostById(int id);
        Task<BlogPost> CreatePost(BlogPost post);
        Task<BlogPost?> UpdatePost(BlogPost post);
        Task<bool> DeletePost(int id);
        Task<Comment?> AddComment(int postId, Comment comment);
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
        Task<Comment?> GetCommentById(int postId, int commentId);
    }
}
