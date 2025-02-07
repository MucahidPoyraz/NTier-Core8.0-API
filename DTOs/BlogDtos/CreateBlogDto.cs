namespace DTOs.BlogDtos
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}
