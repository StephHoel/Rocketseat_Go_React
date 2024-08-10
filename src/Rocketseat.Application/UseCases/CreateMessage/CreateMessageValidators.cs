using FluentValidation;
using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Entities;
using Rocketseat.Domain.Shared;

namespace Rocketseat.Application.UseCases.CreateMessage;

public class CreateMessageValidators : AbstractValidator<Message>
{
    public CreateMessageValidators(IStringLocalizer<ErrorMessages> stringLocalizer)
    {
        RuleFor(entity => entity.MessageText)
            .NotEmpty()
            .WithMessage(stringLocalizer["MessageEmpty"]);
    }
}