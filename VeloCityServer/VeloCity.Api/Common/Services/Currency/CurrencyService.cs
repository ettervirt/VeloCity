using System.Net.Http;
using System.Text.Json;
using VeloCity.Api.Common.Exceptions;

namespace VeloCity.Api.Common.Services.Currency
{
    public class CurrencyService(HttpClient httpClient) : ICurrencyService
    {
        private record NbpResponse(List<NbpRate> Rates);
        private record NbpRate(decimal Mid);
        public async Task<decimal> GetExchangeRateAsync(string currencyCode, CancellationToken ct)
        {
            if (currencyCode.ToUpper() == "PLN") return 1.0m;
            var url = $"https://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/?format=json";

            try
            {
                var response = await httpClient.GetAsync(url, ct);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Błąd podczas pobierania kursu z NBP.");

                var content = await response.Content.ReadAsStringAsync(ct);

                var data = JsonSerializer.Deserialize<NbpResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data?.Rates?[0].Mid ?? throw new Exception("Nie znaleziono kursu.");
            }
            catch
            {
                throw new AppException("Usluga walutowa chwilowo niedostepna.");
            }
        }
    }
}
