using AutoMapper;
using Chatter.Application.Models;
using Chatter.Domain.Dtos;
using Chatter.Domain.Entities;

namespace Chatter.Application.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserMessage, Message>().ReverseMap();
        CreateMap<MessageDto, Message>().ReverseMap();
    }
}