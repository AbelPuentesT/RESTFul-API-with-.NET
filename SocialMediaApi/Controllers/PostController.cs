using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nest;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMediaApi.Responses;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery]PostQueryFilters filters)
        {
            var posts =  _postService.GetPosts(filters);

            //mapeo de entidades con automapper
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);

            //Mapeo manual de entidades
            //var postsDTO = posts.Select(x => new PostDTO
            //{
            //    PostId = x.PostId,
            //    UserId = x.UserId,
            //    Date =x.Date,
            //    Description = x.Description,
            //    Image = x.Image
            //});

            return Ok(postsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post =  await _postService.GetPost(id);

            //mapeo de entidades con automapper
            var postDTO = _mapper.Map<PostDTO>(post);

            //Mapeo manual de entidades
            //var postDTO = new PostDTO { 
            //    PostId = post.PostId, 
            //    UserId = post.UserId, 
            //    Date= post.Date, 
            //    Description= post.Description,
            //    Image= post.Image
            //};

            return Ok(postDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostPost(PostDTO postDTO, [FromQuery] PostQueryFilters filters)
        {

            var post = _mapper.Map<Post>(postDTO);
            var posts =  _postService.GetPosts(filters);
            post.Id = posts.Count() + 1;
            //var post = new Post
            //{
            //    UserId = postDTO.UserId,
            //    Date = postDTO.Date,
            //    Description = postDTO.Description,
            //    Image = postDTO.Image
            //};
            await _postService.InsertPost(post);
            var postDto = _mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDto);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> PutPost(int id, PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            post.Id = id;
            _postService.UpdatePost(post);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePost(id);
            return Ok();
        }

    }
}
