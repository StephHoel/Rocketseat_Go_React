using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Domain.Shared;
using Rocketseat.Exceptions;

namespace Rocketseat.Application.UseCases.MarkMessageAsAnswered;

public class MarkMessageAsAnsweredUseCase
{
    private readonly IMessageRepository _repository;
    private readonly IStringLocalizer<ErrorMessages> _localizer;

    public MarkMessageAsAnsweredUseCase(IMessageRepository repository,
                                        IStringLocalizer<ErrorMessages> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task Execute(Guid roomId, Guid messageId)
    {
        var message = await _repository.GetRoomMessageById(roomId, messageId)
            ?? throw new NotFoundException(_localizer["RoomOrMessagesNotFound"]);

        if (message.Answered)
            throw new InvalidException(_localizer["MessageAnswered"]);

        message.Answered = true;

        await _repository.UpdateMessage(message);
    }
}