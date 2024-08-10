using Rocketseat.Domain.Entities;

namespace Rocketseat.Domain.Interfaces;

public interface IMessageRepository
{
    Task<IEnumerable<Message>?> GetRoomMessagesByRoomId(Guid roomId);

    Task<Message?> GetRoomMessageById(Guid roomId, Guid messageId);

    Task<Guid> CreateMessage(Message message);

    Task UpdateMessage(Message message);
}