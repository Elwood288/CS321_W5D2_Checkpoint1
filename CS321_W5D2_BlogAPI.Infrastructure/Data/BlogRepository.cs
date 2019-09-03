using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _dbContext;

        public BlogRepository(AppDbContext dbContext) 
        {
            // TODO: inject AppDbContext
            //DONE
            _dbContext = dbContext;
        }

        public IEnumerable<Blog> GetAll()
        {
            // TODO: Retrieve all blgs. Include Blog.User.
            //DONE
            return _dbContext.Blogs
               .Include(a => a.User)
               .ToList();
        }

        public Blog Get(int id)
        {
            // TODO: Retrieve the blog by id. Include Blog.User.
            //DONE
            return _dbContext.Blogs
               .Include(a => a.User)
               .SingleOrDefault(b => b.Id == id);
        }

        public Blog Add(Blog blog)
        {
            // TODO: Add new blog
            //DONE
            _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();
            return blog;
        }

        public Blog Update(Blog updatedItem)
        {
            // TODO: update blog
            //DONE
            var currentItem = _dbContext.Blogs.Find(updatedItem.Id);
            if (currentItem == null) return null;
            _dbContext.Entry(currentItem)
                .CurrentValues
                .SetValues(updatedItem);

            _dbContext.Blogs.Update(currentItem);
            _dbContext.SaveChanges();
            return currentItem;
        }

        public void Remove(Blog blog)
        {
            // TODO: remove blog
            //DONE
            _dbContext.Remove(blog);
            _dbContext.SaveChanges();
        }
    }
}
