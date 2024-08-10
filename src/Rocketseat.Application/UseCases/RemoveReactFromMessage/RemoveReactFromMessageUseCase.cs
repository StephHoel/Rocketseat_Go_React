using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Domain.Shared;
using Rocketseat.Exceptions;

namespace Rocketseat.Application.UseCases.RemoveReactFromMessage;

public class RemoveReactFromMessageUseCase
{
    private readonly IMessageRepository _repository;
    private readonly IStringLocalizer<ErrorMessages> _localizer;

    public RemoveReactFromMessageUseCase(IMessageRepository repository,
                                         IStringLocalizer<ErrorMessages> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task Execute(Guid roomId, Guid messageId)
    {
        var message = await _repository.GetRoomMessageById(roomId, messageId)
            ?? throw new NotFoundException(_localizer["RoomOrMessagesNotFound"]);

        if (message.ReactionCount == 0)
            throw new InvalidException(_localizer["ReactEmpty"]);

        message.ReactionCount--;

        await _repository.UpdateMessage(message);
    }
}