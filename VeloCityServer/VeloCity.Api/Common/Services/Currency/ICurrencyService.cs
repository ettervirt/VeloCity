namespace VeloCity.Api.Common.Services.Currency
{
    public interface ICurrencyService
    {
        Task<decimal> GetExchangeRateAsync(string currencyCode, CancellationToken ct);
    }
}
