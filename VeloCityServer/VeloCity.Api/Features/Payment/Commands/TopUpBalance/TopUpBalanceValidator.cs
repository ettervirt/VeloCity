using FluentValidation;

namespace VeloCity.Api.Features.Payment.Commands.TopUpBalance;

public class TopUpBalanceValidator : AbstractValidator<TopUpBalanceCommand>
{
    public TopUpBalanceValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Top-up amount must be greater than zero.")
            .LessThanOrEqualTo(1000)
            .WithMessage("Maximum single top-up amount is 1000.");
        RuleFor(x => x.PaymentMethod)
            .IsInEnum()
            .WithMessage("Unsupported payment method.");
        RuleFor(x => x.Currency)
            .IsInEnum()
            .WithMessage("Unsupported currency.");
    }
}
