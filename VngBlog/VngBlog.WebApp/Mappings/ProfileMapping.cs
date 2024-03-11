using AutoMapper;
using VngBlog.Contract.Shared.Dtos.Categories;
using VngBlog.Contract.Shared.Dtos.Posts;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.WebApp.Mappings
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<CreateUpdatePostDto, Post>().ReverseMap();
            CreateMap<PostDto, Post>().ReverseMap();
            CreateMap<PostInListDto, Post>().ReverseMap();


            CreateMap<CreateUpdateCategoryDto, Category>().ReverseMap();



        }
    }
}
