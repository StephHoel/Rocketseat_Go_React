using Rocketseat.Domain.Entities;

namespace Rocketseat.Application.UseCases.GetRooms;

public class GetRoomsResponse
{
    public IEnumerable<Room> Rooms { get; set; }
}