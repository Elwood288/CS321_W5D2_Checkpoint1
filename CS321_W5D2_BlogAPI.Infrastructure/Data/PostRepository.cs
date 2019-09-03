using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;
        public PostRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Post Get(int id)
        {
            // TODO: Implement Get(id). Include related Blog and Blog.User
            //DONE
            return _dbContext.Posts
                .Include(a => a.Blog)
              .Include(a => a.Blog.User)
              .SingleOrDefault(b => b.Id == id);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            // TODO: Implement GetBlogPosts, return all posts for given blog id
            // TODO: Include related Blog and AppUser
            //DONE
            return _dbContext.Posts
               .Include(a => a.Blog)
               .Include(a => a.Blog.User)
               .Where(a => a.BlogId == blogId) 
               .ToList();
        }

        public Post Add(Post Post)
        {
            // TODO: add Post
            //DONE
            _dbContext.Posts.Add(Post);
            _dbContext.SaveChanges();
            return Post;
        }

        public Post Update(Post updatedPost)
        {
            // TODO: update Post
            //DONE
              var currentPost = _dbContext.Posts.Find(updatedPost.Id);
            if (currentPost == null) return null;
            _dbContext.Entry(currentPost)
                .CurrentValues
                .SetValues(updatedPost);

            _dbContext.Posts.Update(currentPost);
            _dbContext.SaveChanges();
            return currentPost;
        }

        public IEnumerable<Post> GetAll()
        {
            // TODO: get all posts
            //DONE
            return _dbContext.Posts
                .Include(p => p.Comments)
                .ToList();
        }

        public void Remove(Post post)
        {
            // TODO: remove Post
            //DONE
            _dbContext.Remove(post);
            _dbContext.SaveChanges();
        }

    }
}
