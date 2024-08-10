using AutoMapper;
using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Entities;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Domain.Shared;
using Rocketseat.Exceptions;

namespace Rocketseat.Application.UseCases.CreateRoom;

public class CreateRoomUseCase
{
    private readonly IRoomRepository _repository;
    private readonly CreateRoomValidators _validator;

    private readonly IMapper _mapper;

    public CreateRoomUseCase(IRoomRepository roomRepository,
                             IMapper mapper,
                             IStringLocalizer<ErrorMessages> stringLocalizer)
    {
        _repository = roomRepository;
        _mapper = mapper;

        _validator = new CreateRoomValidators(stringLocalizer);
    }

    public async Task<CreateRoomResponse> Execute(CreateRoomRequest request)
    {
        var entity = _mapper.Map<Room>(request);

        var validator = _validator.Validate(entity);

        if (!validator.IsValid)
            throw new ErrorOnValidationException(validator.Errors.Select(error => error.ErrorMessage));

        var result = await _repository.RegisterRoom(entity);

        return _mapper.Map<CreateRoomResponse>(result);
    }
}