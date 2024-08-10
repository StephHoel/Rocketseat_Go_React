using Rocketseat.Domain.Entities;

namespace Rocketseat.Application.UseCases.GetRoomMessages;

public class GetRoomMessagesResponse
{
    public IEnumerable<Message> Messages { get; set; }
}