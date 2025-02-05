using Entity;
using FluentValidation;

namespace BL.ValidationRules
{
    public class BlogValidation : AbstractValidator<Blog>
    {
        public BlogValidation()
        {
            RuleFor(blog => blog.Title)
                .NotEmpty().WithMessage("Başlık zorunludur.")
                .Length(5, 200).WithMessage("Başlık 5 ile 200 karakter arasında olmalıdır.");

            RuleFor(blog => blog.Content)
                .NotEmpty().WithMessage("İçerik zorunludur.");

            RuleFor(blog => blog.CategoryId)
                .NotEmpty().WithMessage("Kategori ID zorunludur.")
                .GreaterThan(0).WithMessage("Kategori ID 0'dan büyük olmalıdır.");
        }
     
    }
}
