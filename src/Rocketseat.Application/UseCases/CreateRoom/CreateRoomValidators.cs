using FluentValidation;
using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Entities;
using Rocketseat.Domain.Shared;

namespace Rocketseat.Application.UseCases.CreateRoom;

public class CreateRoomValidators : AbstractValidator<Room>
{
    public CreateRoomValidators(IStringLocalizer<ErrorMessages> stringLocalizer)
    {
        RuleFor(entity => entity.Theme)
            .NotEmpty()
            .WithMessage(stringLocalizer["ThemeEmpty"]);
    }
}