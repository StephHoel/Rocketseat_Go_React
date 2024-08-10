using AutoMapper;
using Rocketseat.Domain.Entities;

namespace Rocketseat.Application.UseCases.CreateMessage;

public class CreateMessageMappings : Profile
{
    public CreateMessageMappings()
    {
        CreateMap<(CreateMessageRequest, Guid), Message>()
            .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.Item1.Message))
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Item2));

        CreateMap<Guid, CreateMessageResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
    }
}