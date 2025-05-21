namespace DTOs.CategoryDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RowVersion { get; set; } // byte[] yerine string
    }
}
