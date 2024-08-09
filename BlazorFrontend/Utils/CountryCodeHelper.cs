using System.Net.Http;
using System.Text.Json;

namespace BlazorFrontend.Utils
{
    public static class CountryCodeHelper
    {

        private readonly static HttpClient _httpClient = new HttpClient();
        public static async Task<string?> GetCountryCodeFromIp(string ipAddress)
        {
            try
            {
                var ipString = await _httpClient.GetStringAsync($"https://api.cleantalk.org/?method_name=ip_info&ip={ipAddress}");

                using (var jsonDoc = JsonDocument.Parse(ipString))
                {
                    if (jsonDoc.RootElement.TryGetProperty("data", out var dataElement) &&
                        dataElement.TryGetProperty(ipAddress, out var ipElement) &&
                        ipElement.TryGetProperty("country_code", out var countryCodeElement))
                    {
                        return countryCodeElement.GetString();
                    }
                }
            }
            catch
            {
                throw;
            }
            return null;
        }
    }
}
