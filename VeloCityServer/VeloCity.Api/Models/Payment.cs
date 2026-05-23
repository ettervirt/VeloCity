using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Models;

public class Payment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public decimal Amount { get; set; } = 0.00m;
    public Currency Currency { get; set; } = Currency.PLN;
    public decimal ExchangeRate { get; set; }
    public decimal AmountInBaseCurrency { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Card;
    public string TransactionId { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Completed;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
