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
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<PaginatedList<Category>, PaginatedResponseDto<CategoryDto>>().ReverseMap();
        }
    }
}
