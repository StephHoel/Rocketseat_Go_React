using Rocketseat.Domain.Entities;

namespace Rocketseat.Domain.Interfaces;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetRooms();

    Task<Room> GetRoom(Guid roomId);

    Task<Guid> RegisterRoom(Room room);
}