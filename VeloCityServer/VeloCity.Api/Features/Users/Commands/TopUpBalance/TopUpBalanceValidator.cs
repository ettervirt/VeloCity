using FluentValidation;

namespace VeloCity.Api.Features.Users.Commands.TopUpBalance;

public class TopUpBalanceValidator : AbstractValidator<TopUpBalanceCommand>
{
    public TopUpBalanceValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero")
            .LessThan(1000).WithMessage("At least 1000");
    }
}
