using System.Net.Http.Json;

namespace bbxp.lib.HttpHandlers.Base
{
    public class BaseHttpHandler
    {
        protected readonly HttpClient httpClient;

        public BaseHttpHandler(string baseAddress)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        protected async Task<bool> PostAsync<T>(string url, T objValue)
        {
            var result = await httpClient.PostAsJsonAsync<T>(url, objValue);

            return result != null && result.IsSuccessStatusCode;
        }

        protected async Task<T> GetAsync<T>(string url) => await httpClient.GetFromJsonAsync<T>(url);
    }
}