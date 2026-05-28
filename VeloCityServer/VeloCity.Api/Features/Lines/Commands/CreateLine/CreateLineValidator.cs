using FluentValidation;

namespace VeloCity.Api.Features.Lines.Commands.CreateLine;

public class CreateLineValidator: AbstractValidator<CreateLineCommand>
{
    public CreateLineValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
