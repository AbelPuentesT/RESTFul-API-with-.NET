using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nest;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMediaApi.Responses;
using System.Net;

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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPosts([FromQuery]PostQueryFilters filters)
        {
            var posts =  _postService.GetPosts(filters);
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
            var response= new ApiResponse<IEnumerable<PostDTO>>(postsDTO);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post =  await _postService.GetPost(id);
            var postDTO = _mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostPost(PostDTO postDTO, [FromQuery] PostQueryFilters filters)
        {
            var post = _mapper.Map<Post>(postDTO);
            var posts =  _postService.GetPosts(filters);
            post.Id = posts.Count() + 1;
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
            var result = await _postService.UpdatePost(post);
            var response= new ApiResponse<bool>(result);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result=await _postService.DeletePost(id);
            var response= new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
