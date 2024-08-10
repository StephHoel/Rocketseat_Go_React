using AutoMapper;
using Microsoft.Extensions.Localization;
using Rocketseat.Application.Services;
using Rocketseat.Domain.Entities;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Domain.Shared;
using Rocketseat.Exceptions;

namespace Rocketseat.Application.UseCases.CreateMessage;

public class CreateMessageUseCase
{
    private readonly WebSocketHandler _webSocketHandler;
    private readonly CreateMessageValidators _validator;

    private readonly IStringLocalizer<ErrorMessages> _localizer;
    private readonly IRoomRepository _roomRepository;
    private readonly IMessageRepository _repository;
    private readonly IMapper _mapper;

    public CreateMessageUseCase(WebSocketHandler webSocketHandler,
                                IRoomRepository roomRepository,
                                IMessageRepository repository,
                                IMapper mapper,
                                IStringLocalizer<ErrorMessages> stringLocalizer)
    {
        _webSocketHandler = webSocketHandler;
        _roomRepository = roomRepository;
        _repository = repository;
        _mapper = mapper;
        _localizer = stringLocalizer;

        _validator = new CreateMessageValidators(stringLocalizer);
    }

    public async Task<CreateMessageResponse> Execute(CreateMessageRequest request, Guid roomId)
    {
        var entity = _mapper.Map<Message>((request, roomId));

        Validate(entity);

        var result = await _repository.CreateMessage(entity);

        await _webSocketHandler.SendMessageToRoomAsync(entity.RoomId, entity.MessageText);

        return _mapper.Map<CreateMessageResponse>(result);
    }

    private async void Validate(Message entity)
    {
        var validator = _validator.Validate(entity);

        var validRoom = await _roomRepository.GetRoom(entity.RoomId);

        var errors = new List<string>();

        if (!validator.IsValid)
            errors.AddRange(validator.Errors.Select(error => error.ErrorMessage));

        if (validRoom is null)
            errors.Add(_localizer["RoomNotFound"]);

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);

        entity.Room = validRoom!;
    }
}