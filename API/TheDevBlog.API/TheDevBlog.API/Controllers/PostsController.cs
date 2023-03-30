using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Data;
using TheDevBlog.API.Models.DTO;
using TheDevBlog.API.Models.Entity;

namespace TheDevBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly TheDevBlogDbContext dbContext;

        public PostsController(TheDevBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await dbContext.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPostById")] //use for AddPost

        public async Task<IActionResult> GetPostById(Guid id)
        {
            var posts=await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if(posts!=null)
            {
                return Ok(posts);
            }
            return NotFound();
            
        }
        [HttpPost]

        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            //convert DTO to Entity

            var post = new Post()
            {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Summry = addPostRequest.Summry,
                UrlHandle = addPostRequest.UrlHandle,
                FeaturedImageUrl = addPostRequest.FeaturedImageUrl,
                Visible = addPostRequest.Visible,
                Author = addPostRequest.Author,
                PublishDate = addPostRequest.PublishDate,
                UpdatedDate = addPostRequest.UpdatedDate
            };

            post.Id = Guid.NewGuid();
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPostById),new {id = post.Id},post);

        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdatePost([FromRoute] Guid id,UpdatePostRequest updatePostRequest)
        {
            //convert DTO to Entity

          /*  var post = new Post()
            {
                Title = updatePostRequest.Title,
                Content = updatePostRequest.Content,
                Summry = updatePostRequest.Summry,
                UrlHandle = updatePostRequest.UrlHandle,
                FeaturedImageUrl = updatePostRequest.FeaturedImageUrl,
                Visible = updatePostRequest.Visible,
                Author = updatePostRequest.Author,
                PublishDate = updatePostRequest.PublishDate,
                UpdatedDate = updatePostRequest.UpdatedDate
            };
          */
            //check if exist
            var existingpost = await dbContext.Posts.FindAsync(id);

            if(existingpost != null)
            {
                existingpost.Title = updatePostRequest.Title;
                existingpost.Content = updatePostRequest.Content;
                existingpost.Summry = updatePostRequest.Summry;
                existingpost.UrlHandle = updatePostRequest.UrlHandle;
                existingpost.FeaturedImageUrl = updatePostRequest.FeaturedImageUrl;
                existingpost.Visible = updatePostRequest.Visible;
                existingpost.Author = updatePostRequest.Author;
                existingpost.PublishDate = updatePostRequest.PublishDate;
                existingpost.UpdatedDate = updatePostRequest.UpdatedDate;
                await dbContext.SaveChangesAsync();
                return Ok(existingpost);

            }
            return NotFound();


        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingpost = await dbContext.Posts.FindAsync(id);

            if (existingpost != null)
            {
               dbContext.Posts.Remove(existingpost);    
                await dbContext.SaveChangesAsync();
                return Ok(existingpost);

            }
            return NotFound();
        }




    }
}
