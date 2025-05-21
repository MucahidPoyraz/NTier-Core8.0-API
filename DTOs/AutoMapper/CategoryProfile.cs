using AutoMapper;
using Common.Models;
using DTOs.CategoryDtos;
using DTOs.PageDtos;
using Entity;

namespace DTOs.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto , Category>().ReverseMap();

            CreateMap<Category, UpdateCategoryDto>()
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => Convert.ToBase64String(src.RowVersion)));
            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => Convert.FromBase64String(src.RowVersion)));

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => Convert.ToBase64String(src.RowVersion)));
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => Convert.FromBase64String(src.RowVersion)));

            CreateMap<PaginatedList<Category>, PaginatedResponseDto<CategoryDto>>().ReverseMap();
        }
    }
}
