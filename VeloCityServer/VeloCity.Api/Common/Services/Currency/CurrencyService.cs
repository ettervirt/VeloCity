using System.Text.Json;
using VeloCity.Api.Common.Exceptions;

namespace VeloCity.Api.Common.Services.Currency;

public class CurrencyService(HttpClient httpClient) : ICurrencyService
{
    private record NbpResponse(List<NbpRate> Rates);
    private record NbpRate(decimal Mid);

    public async Task<decimal> GetExchangeRateAsync(string currencyCode, CancellationToken ct)
    {
        if (currencyCode.Equals("PLN", StringComparison.OrdinalIgnoreCase))
            return 1.0m;

        var url = $"https://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/?format=json";

        try
        {
            var response = await httpClient.GetAsync(url, ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new AppException($"Failed to fetch exchange rate for {currencyCode}.", 502);
            }

            var content = await response.Content.ReadAsStringAsync(ct);

            var data = JsonSerializer.Deserialize<NbpResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data?.Rates?.FirstOrDefault()?.Mid
                   ?? throw new AppException("Exchange rate not found in the response.", 502);
        }
        catch (AppException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new AppException("Currency service is temporarily unavailable.", 503);
        }
    }
}
