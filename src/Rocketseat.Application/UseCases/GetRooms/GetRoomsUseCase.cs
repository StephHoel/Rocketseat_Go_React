using AutoMapper;
using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Domain.Shared;
using Rocketseat.Exceptions;

namespace Rocketseat.Application.UseCases.GetRooms;

public class GetRoomsUseCase
{
    private readonly IRoomRepository _repository;
    private readonly IMapper _mapper;

    private readonly IStringLocalizer<ErrorMessages> _localizer;

    public GetRoomsUseCase(IRoomRepository repository,
                           IMapper mapper,
                           IStringLocalizer<ErrorMessages> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<GetRoomsResponse> Execute()
    {
        var response = await _repository.GetRooms();

        return response is null ?
            throw new NotFoundException(_localizer["RoomsNotFound"])
            : _mapper.Map<GetRoomsResponse>(response);
    }
}