using AutoMapper;
using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Domain.Shared;
using Rocketseat.Exceptions;

namespace Rocketseat.Application.UseCases.GetRoomMessage;

public class GetRoomMessageUseCase
{
    private readonly IMessageRepository _repository;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ErrorMessages> _localizer;
    public GetRoomMessageUseCase(IMessageRepository repository,
                                 IStringLocalizer<ErrorMessages> localizer,
                                 IMapper mapper)
    {
        _repository = repository;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<GetRoomMessageResponse> Execute(Guid roomId, Guid messageId)
    {
       var message = await _repository.GetRoomMessageById(roomId, messageId)
            ?? throw new NotFoundException(_localizer["RoomOrMessagesNotFound"]);

        return _mapper.Map<GetRoomMessageResponse>(message);
    }
}