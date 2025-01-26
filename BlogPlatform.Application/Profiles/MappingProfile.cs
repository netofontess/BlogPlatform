using AutoMapper;
using BlogPlatform.Application.DTOs;
using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostDto, BlogPost>().ReverseMap();

            CreateMap<CommentDto, Comment>().ReverseMap();
        }
    }
}
