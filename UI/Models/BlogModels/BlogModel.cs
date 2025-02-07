using UI.Models.CategoryModels;

namespace UI.Models.BlogModels
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
