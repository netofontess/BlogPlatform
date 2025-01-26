using AutoMapper;
using BlogPlatform.Application.DTOs;
using BlogPlatform.Application.Interfaces.Repositories;
using BlogPlatform.Application.Interfaces.Services;
using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public PostService(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostDto>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPosts();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<PostDto> GetPostById(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null) return null;

            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> CreatePost(PostDto postDto)
        {
            var post = _mapper.Map<BlogPost>(postDto);
            var createdPost = await _postRepository.CreatePost(post);
            return _mapper.Map<PostDto>(createdPost);
        }

        public async Task<CommentDto> AddComment(int postId, CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            var addedComment = await _postRepository.AddComment(postId, comment);
            return _mapper.Map<CommentDto>(addedComment);
        }
    }
}
