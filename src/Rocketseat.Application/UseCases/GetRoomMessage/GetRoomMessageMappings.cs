using AutoMapper;
using Rocketseat.Domain.Entities;

namespace Rocketseat.Application.UseCases.GetRoomMessage;

public class GetRoomMessageMappings : Profile
{
    public GetRoomMessageMappings()
    {
        CreateMap<Message, GetRoomMessageResponse>()
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src));
    }
}