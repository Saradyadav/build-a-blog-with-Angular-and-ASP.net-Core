using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Models.Entity;

namespace TheDevBlog.API.Data
{
    public class TheDevBlogDbContext : DbContext
    {
        public TheDevBlogDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSet
        public DbSet<Post> Posts { get; set; }
        
    }
}
