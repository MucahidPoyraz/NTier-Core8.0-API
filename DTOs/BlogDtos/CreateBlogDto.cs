using Microsoft.AspNetCore.Http;

namespace DTOs.BlogDtos
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
