using BlogPlatform.Application.DTOs;
using BlogPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Gets all posts with their titles and comment counts.
        /// </summary>
        /// <returns>A list of posts.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), 200)]
        public async Task<IActionResult> Get()
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="newPost">The new post to be created.</param>
        /// <returns>The newly created post.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PostDto), 201)]
        public async Task<IActionResult> CreatePost([FromBody] PostDto newPost)
        {
            if (newPost == null)
                return BadRequest("Post data is null");

            var createdPost = await _postService.CreatePost(newPost);
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
        }

        /// <summary>
        /// Gets a specific post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post.</param>
        /// <returns>The post with the given ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PostDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostById(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        /// <summary>
        /// Adds a comment to a specific post.
        /// </summary>
        /// <param name="id">The ID of the post.</param>
        /// <param name="newComment">The comment to be added.</param>
        /// <returns>The newly added comment.</returns>
        [HttpPost("{id}/comments")]
        [ProducesResponseType(typeof(CommentDto), 201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddComment(int id, [FromBody] CommentDto newComment)
        {
            if (newComment == null)
                return BadRequest("Comment data is null");

            var addedComment = await _postService.AddComment(id, newComment);
            if (addedComment == null)
                return NotFound();

            return CreatedAtAction(nameof(GetPostById), new { id = id }, addedComment);
        }
    }
}
