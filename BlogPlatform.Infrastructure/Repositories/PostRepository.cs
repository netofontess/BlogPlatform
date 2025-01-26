using BlogPlatform.Application.Interfaces.Repositories;
using BlogPlatform.Domain.Entities;
using BlogPlatform.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllPosts()
        {
            return await _context.BlogPost.Include(p => p.Comments).ToListAsync();
        }

        public async Task<BlogPost?> GetPostById(int id)
        {
            return await _context.BlogPost
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<BlogPost> CreatePost(BlogPost post)
        {
            _context.BlogPost.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> UpdatePost(BlogPost post)
        {
            var existingPost = await _context.BlogPost.FirstOrDefaultAsync(p => p.Id == post.Id);
            if (existingPost == null) return null;

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            await _context.SaveChangesAsync();
            return existingPost;
        }

        public async Task<bool> DeletePost(int id)
        {
            var post = await _context.BlogPost.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) return false;

            _context.BlogPost.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Comment?> AddComment(int postId, Comment comment)
        {
            var post = await _context.BlogPost.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return null;

            post.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            var post = await _context.BlogPost.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);
            return post?.Comments ?? new List<Comment>();
        }

        public async Task<Comment?> GetCommentById(int postId, int commentId)
        {
            var post = await _context.BlogPost.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);
            return post?.Comments.FirstOrDefault(c => c.Id == commentId);
        }
    }
}
