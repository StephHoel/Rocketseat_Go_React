using Rocketseat.Domain.Entities;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Infra.Context;

namespace Rocketseat.Infra.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly RocketseatDbContext _dbContext;

    public RoomRepository(RocketseatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetRooms()
        => [.. _dbContext.Rooms];

    public async Task<Room> GetRoom(Guid roomId)
        => _dbContext.Rooms.First(x => x.Id == roomId);

    public async Task<Guid> RegisterRoom(Room room)
    {
        _dbContext.Rooms.Add(room);
        _dbContext.SaveChanges();

        return room.Id;
    }
}