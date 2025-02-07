namespace UI.Models.BlogModels
{
    public class CreateBlogModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
    }
}
