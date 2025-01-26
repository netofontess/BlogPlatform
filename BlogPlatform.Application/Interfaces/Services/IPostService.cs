using BlogPlatform.Application.DTOs;
using BlogPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BlogPlatform.Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPosts();
        Task<PostDto> GetPostById(int id);
        Task<PostDto> CreatePost(PostDto postDto);
        Task<CommentDto> AddComment(int postId, CommentDto commentDto);
    }
}