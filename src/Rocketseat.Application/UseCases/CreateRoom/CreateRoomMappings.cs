using AutoMapper;
using Rocketseat.Domain.Entities;

namespace Rocketseat.Application.UseCases.CreateRoom;

public class CreateRoomMappings : Profile
{
    public CreateRoomMappings()
    {
        CreateMap<CreateRoomRequest, Room>()
            .ForMember(dest => dest.Theme, opt => opt.MapFrom(src => src.Theme.Trim()));

        CreateMap<Guid, CreateRoomResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
    }
}