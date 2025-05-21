using Microsoft.AspNetCore.Http;

namespace DTOs.BlogDtos
{
    public class UpdateBlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
