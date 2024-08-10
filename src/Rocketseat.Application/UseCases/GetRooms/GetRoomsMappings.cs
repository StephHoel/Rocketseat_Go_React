using AutoMapper;
using Rocketseat.Domain.Entities;

namespace Rocketseat.Application.UseCases.GetRooms;

public class GetRoomsMappings : Profile
{
    public GetRoomsMappings()
    {
        CreateMap<IEnumerable<Room>, GetRoomsResponse>()
            .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src));
    }
}