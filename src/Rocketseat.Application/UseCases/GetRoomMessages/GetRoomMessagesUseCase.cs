using AutoMapper;
using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Domain.Shared;
using Rocketseat.Exceptions;

namespace Rocketseat.Application.UseCases.GetRoomMessages;

public class GetRoomMessagesUseCase
{
    private readonly IMessageRepository _repository;
    private readonly IMapper _mapper;

    private readonly IStringLocalizer<ErrorMessages> _localizer;

    public GetRoomMessagesUseCase(IMessageRepository repository,
                                  IMapper mapper,
                                  IStringLocalizer<ErrorMessages> stringLocalizer)
    {
        _repository = repository;
        _mapper = mapper;
        _localizer = stringLocalizer;
    }

    public async Task<GetRoomMessagesResponse> Execute(Guid roomId)
    {
        var roomMessages = await _repository.GetRoomMessagesByRoomId(roomId) 
            ?? throw new NotFoundException(_localizer["RoomOrMessagesNotFound"]);

        return _mapper.Map<GetRoomMessagesResponse>(roomMessages);
    }
}