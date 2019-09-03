using System;
using System.Collections.Generic;
using CS321_W5D2_BlogAPI.Core.Models;

namespace CS321_W5D2_BlogAPI.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserService _userService;
        public static DateTime Now { get; }

        public PostService(IPostRepository postRepository, IBlogRepository blogRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userService = userService;

        }

        public Post Add(Post newPost)
        {
            // TODO: Prevent users from adding to a blog that isn't theirs
            //     Use the _userService to get the current users id.
            //     You may have to retrieve the blog in order to check user id
            
            var currentBlog = _blogRepository.Get(newPost.BlogId);
            DateTime postDate = Now;

            if (_userService.CurrentUserId == currentBlog.UserId)
            {
                // TODO: assign the current date to DatePublished
                //DONE
                newPost.DatePublished = Now;
                return _postRepository.Add(newPost);
            }
            else
            {
                return null;
            }
        }

        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }
        
        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            return _postRepository.GetBlogPosts(blogId);
        }

        public void Remove(int id)
        {
            Post post = Get(id);
            // TODO: prevent user from deleting from a blog that isn't theirs
            //DONE
            var currentBlog = _blogRepository.Get(post.BlogId);

            if (_userService.CurrentUserId == currentBlog.UserId)
            {
                _postRepository.Remove(post);
            }
           
            
        }

        public Post Update(Post updatedPost)
        {
            // TODO: prevent user from updating a blog that isn't theirs
            //DONE
            var currentBlog = _blogRepository.Get(updatedPost.BlogId);

            if (_userService.CurrentUserId == currentBlog.UserId)
            {
                return _postRepository.Update(updatedPost);
            }
            else
            {
                return null;
            }
            
        }

    }
}
