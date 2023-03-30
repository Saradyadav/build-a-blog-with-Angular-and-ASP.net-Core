namespace TheDevBlog.API.Models.Entity
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summry { get; set; }
        public string UrlHandle { get; set; }
        public string FeaturedImageUrl { get; set; }
        public bool Visible { get; set; }   
        public string Author { get; set; }  
        public DateTime PublishDate { get; set; }
        public DateTime UpdatedDate { get; set; }  

    }
}
