﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IRepository<Post> _postRepository;
        //private readonly IRepository<User> _userRepository;
        //private readonly IRepository<Comment> _commentRepository;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public IEnumerable<Post> GetPosts()
        {
            return _unitOfWork.PostRepository.GetAll();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if(user == null)
            {
                throw new BusinessException("User dosn't  exits");
            }
            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if (userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(x=>x.Date).FirstOrDefault();
                if((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish the post");
                }
            }
            if (post.Description.Contains("sexo"))
            {
                throw new BusinessException("Content not allowed");
            }
            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
             _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
