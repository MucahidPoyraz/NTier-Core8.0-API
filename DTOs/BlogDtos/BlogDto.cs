using DTOs.CategoryDtos;

namespace DTOs.BlogDtos
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
