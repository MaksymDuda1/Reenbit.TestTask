using AutoMapper;
using Chatter.Application.Models;
using Chatter.Domain.Dtos;
using Chatter.Domain.Entities;

namespace Chatter.Application.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MessageDto, Message>().ReverseMap();
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt
                => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PhotoPath, opt
                => opt.MapFrom(src => src.PhotoPath));
    }
}