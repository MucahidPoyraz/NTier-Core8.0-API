using AutoMapper;
using Common.Models;
using DTOs.BlogDtos;
using DTOs.CategoryDtos;
using DTOs.PageDtos;
using Entity;

namespace DTOs.AutoMapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogDto, Blog>().ReverseMap();
            CreateMap<CreateBlogDto, Blog>().ReverseMap();
            CreateMap<UpdateBlogDto, Blog>().ReverseMap();

            CreateMap<PaginatedList<Blog>, PaginatedResponseDto<BlogDto>>().ReverseMap();
        }
    }
}
