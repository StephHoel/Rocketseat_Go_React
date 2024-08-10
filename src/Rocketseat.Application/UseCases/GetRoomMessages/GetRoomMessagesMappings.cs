using AutoMapper;
using Rocketseat.Domain.Entities;

namespace Rocketseat.Application.UseCases.GetRoomMessages;

public class GetRoomMessagesMappings : Profile
{
    public GetRoomMessagesMappings()
    {
        CreateMap<IEnumerable<Message>, GetRoomMessagesResponse>()
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src));
    }
}