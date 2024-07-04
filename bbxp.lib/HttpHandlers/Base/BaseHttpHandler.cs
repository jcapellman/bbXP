using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace bbxp.lib.HttpHandlers.Base
{
    public class BaseHttpHandler
    {
        protected readonly HttpClient httpClient;

        public BaseHttpHandler(string baseAddress, string? token = null)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            if (!string.IsNullOrEmpty(token)) {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        protected async Task<bool> PostAsync<T>(string url, T objValue)
        {
            var result = await httpClient.PostAsJsonAsync<T>(url, objValue);

            return result != null && result.IsSuccessStatusCode;
        }

        protected async Task<string> PostReturnStringAsync<T>(string url, T objValue)
        {
            var response = await httpClient.PostAsJsonAsync<T>(url, objValue);

            if (response != null && response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }

        protected async Task<bool> PatchAsync<T>(string url, T objValue)
        {
            var result = await httpClient.PatchAsJsonAsync<T>(url, objValue);

            return result != null && result.IsSuccessStatusCode;
        }

        protected async Task<T?> GetAsync<T>(string url) => await httpClient.GetFromJsonAsync<T>(url);
    }
}