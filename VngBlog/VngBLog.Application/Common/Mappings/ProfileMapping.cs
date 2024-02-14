using AutoMapper;

using VngBlog.Contract.Shared.Dtos.Posts;
using VngBlog.Domain.Entities.Systems;

namespace VngBLog.Application.Common.Mappings
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<CreateUpdatePostDto, Post>().ReverseMap();
            CreateMap<PostDto, Post>().ReverseMap();
            CreateMap<PostInListDto, Post>().ReverseMap();
     
        }
    }
}
