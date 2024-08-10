using Microsoft.EntityFrameworkCore;
using Rocketseat.Domain.Entities;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Infra.Context;

namespace Rocketseat.Infra.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly RocketseatDbContext _dbContext;

    public MessageRepository(RocketseatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Message>?> GetRoomMessagesByRoomId(Guid roomId)
        => [.. _dbContext.Messages.Where(x => x.RoomId == roomId)];

    public async Task<Message?> GetRoomMessageById(Guid roomId, Guid messageId)
        => _dbContext.Messages.Include(m => m.Room).Where(x => x.Id == messageId && x.RoomId == roomId).FirstOrDefault();

    public async Task<Guid> CreateMessage(Message message)
    {
        _dbContext.Messages.Add(message);
        _dbContext.SaveChanges();

        return message.Id;
    }

    public async Task UpdateMessage(Message message)
    {
        _dbContext.Messages.Update(message);
        _dbContext.SaveChanges();
    }
}