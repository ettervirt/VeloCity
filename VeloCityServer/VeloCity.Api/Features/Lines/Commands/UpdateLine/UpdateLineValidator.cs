using FluentValidation;

namespace VeloCity.Api.Features.Lines.Commands.UpdateLine
{
    public class UpdateLineValidator : AbstractValidator<UpdateLineCommand>
    {
        public UpdateLineValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
