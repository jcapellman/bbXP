using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;

using Newtonsoft.Json;

namespace bbxp.CommonLibrary.Handlers {
    public abstract class BaseHandler {
        private readonly GlobalSettings _gSettings;

        protected BaseHandler(GlobalSettings globalSettings) {
            _gSettings = globalSettings;
        }

        protected abstract string BaseControllerName();

        private HttpClient GetHttpClient() {
            var handler = new HttpClientHandler();

            var client = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(1) };

            return client;
        }

        private string generateURL(string arguments) => string.IsNullOrEmpty(arguments) ? $"{_gSettings.WebAPIAddress}{BaseControllerName()}" : $"{_gSettings.WebAPIAddress}{BaseControllerName()}?{arguments}";

        internal async Task<TK> GetAsync<T, TK>(T obj) {
            var objStr = JsonConvert.SerializeObject(obj);

            return await GetAsync<TK>(objStr);
        }

        public async Task<T> GetAsync<T>() => await GetAsync<T>(string.Empty);

        protected async Task<T> GetAsync<T>(string urlArguments) {
            var url = generateURL(urlArguments);

            var str = await GetHttpClient().GetStringAsync(url);

            return JsonConvert.DeserializeObject<T>(str);
        }

        protected async void GetAsync(string urlArguments) {
            await GetHttpClient().GetStringAsync(generateURL(urlArguments));
        }

        private static StringContent GetStringContent<T>(T obj) => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        protected async Task<TK> PostAsync<T, TK>(T obj) {
            var response = await GetHttpClient().PostAsync(generateURL(string.Empty), GetStringContent(obj));

            var responseStr = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TK>(responseStr);
        }

        protected async Task<ReturnSet<bool>> DeleteAsync(string urlArguments) {
            var url = generateURL(urlArguments);

            var str = await GetHttpClient().DeleteAsync(url);

            return new ReturnSet<bool>(str.IsSuccessStatusCode);
        }

        protected async Task<TK> PutAsync<T, TK>(T obj) => await PutAsync<T, TK>(string.Empty, obj);

        protected async Task<TK> PutAsync<T, TK>(string urlArguments, T obj) {
            var response = await GetHttpClient().PutAsync(generateURL(string.Empty), GetStringContent(obj));

            var responseStr = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TK>(responseStr);
        }
    }
}