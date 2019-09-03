using System;
using CS321_W5D2_BlogAPI.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CS321_W5D2_BlogAPI.Core.Services;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CS321_W5D2_BlogAPI.Controllers
{
    // TODO: secure controller actions that change data
    //DONE
    [Authorize]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {

        private readonly IPostService _postService;

        // TODO: inject PostService
        //DONE
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // TODO: get posts for blog
        //DONE
        // TODO: allow anyone to get, even if not logged in
        //DONE
        // GET /api/blogs/{blogId}/posts
        [AllowAnonymous]
        [HttpGet("/api/blogs/{blogId}/posts")]
        public IActionResult Get(int blogId)
        {
            // TODO: replace the code below with the correct implementation
            //DONE
            try
            {
                
                var allPosts = _postService
                    .GetBlogPosts(blogId)
                    .ToApiModels();
                return Ok(allPosts);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPosts", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // TODO: get post by id
        //DONE
        // TODO: allow anyone to get, even if not logged in
        //DONE
        // GET api/blogs/{blogId}/posts/{postId}
        [AllowAnonymous]
        [HttpGet("/api/blogs/{blogId}/posts/{postId}")]
        public IActionResult Get(int blogId, int postId)
        {
            // TODO: replace the code below with the correct implementation
            //DONE
            try
            {
                
                var post = _postService
                    .GetBlogPosts(blogId)
                    .ToApiModels()
                    .FirstOrDefault(p => p.Id == postId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetPost", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // TODO: add a new post to blog
        //DONE
        // POST /api/blogs/{blogId}/post
        [HttpPost("/api/blogs/{blogId}/posts")]
        public IActionResult Post(int blogId, [FromBody]PostModel postModel)
        {
            // TODO: replace the code below with the correct implementation
            //DONE
            var post = _postService.Add(postModel.ToDomainModel());
            if (post != null)
                return Ok(post);
            ModelState.AddModelError("Not authorized", "You do not have access to this post.");
            return BadRequest(ModelState);
        }

        // PUT /api/blogs/{blogId}/posts/{postId}
        [HttpPut("/api/blogs/{blogId}/posts/{postId}")]
        public IActionResult Put(int blogId, int postId, [FromBody]PostModel postModel)
        {
            try
            {
                var updatedPost = _postService.Update(postModel.ToDomainModel());
                return Ok(updatedPost);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdatePost", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // TODO: delete post by id
        // DELETE /api/blogs/{blogId}/posts/{postId}
        [HttpDelete("/api/blogs/{blogId}/posts/{postId}")]
        public IActionResult Delete(int blogId, int postId)
        {
            // TODO: replace the code below with the correct implementation
            //DONE
            try
            {
                //to delete a post by id
                _postService.Remove(postId);
                return Ok(_postService.GetAll());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DeletedPost", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
