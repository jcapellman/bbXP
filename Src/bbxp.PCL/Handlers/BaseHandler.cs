using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using bbxp.PCL.Common;
using bbxp.PCL.Settings;

using Newtonsoft.Json;

namespace bbxp.PCL.Handlers {
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

        private string generateURL(string arguments, bool useFallbackWebAPIAddress = false) {
            var webApiAddress = (useFallbackWebAPIAddress ? _gSettings.WebAPIAddress : _gSettings.CachingWebAPIAddress);

            return string.IsNullOrEmpty(arguments)
                ? $"{webApiAddress}{BaseControllerName()}"
                : $"{webApiAddress}{BaseControllerName()}{arguments}";
        }

        internal async Task<TK> GetAsync<T, TK>(T obj) {
            var objStr = JsonConvert.SerializeObject(obj);

            return await GetAsync<TK>(objStr);
        }

        public async Task<T> GetAsync<T>() => await GetAsync<T>(string.Empty);

        private string cleanURL(string url) {
            if (string.IsNullOrEmpty(url)) {
                return url;
            }

            return url[0] + WebUtility.UrlEncode(url.Substring(1));
        }

        protected async Task<dynamic> GetAsync<T>(string urlArguments, bool useFallbackWebAPIAddress = false) {
            try {
                var url = generateURL(cleanURL(urlArguments), useFallbackWebAPIAddress);

                var str = await GetHttpClient().GetStringAsync(url);

                if (string.IsNullOrEmpty(str) && !useFallbackWebAPIAddress) {
                    return await GetAsync<T>(urlArguments, true);
                }

                return (T)JsonConvert.DeserializeObject<T>(str);
            } catch (Exception ex) {
                if (!useFallbackWebAPIAddress) {
                    return await GetAsync<T>(urlArguments, true);
                }

                dynamic response = Activator.CreateInstance(typeof (T));

                response.Exception = ex.ToString();

                return response;
            }
        }

        protected async void GetAsync(string urlArguments) {
            await GetHttpClient().GetStringAsync(generateURL(urlArguments, true));
        }

        private static StringContent GetStringContent<T>(T obj) => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        protected async Task<TK> PostAsync<T, TK>(T obj) {
            var response = await GetHttpClient().PostAsync(generateURL(string.Empty, true), GetStringContent(obj));

            var responseStr = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TK>(responseStr);
        }

        protected async Task<ReturnSet<bool>> DeleteAsync(string urlArguments) {
            var url = generateURL(urlArguments, true);

            var str = await GetHttpClient().DeleteAsync(url);

            return new ReturnSet<bool>(str.IsSuccessStatusCode);
        }

        protected async Task<TK> PutAsync<T, TK>(T obj) => await PutAsync<T, TK>(string.Empty, obj);

        protected async Task<TK> PutAsync<T, TK>(string urlArguments, T obj) {
            var response = await GetHttpClient().PutAsync(generateURL(string.Empty, true), GetStringContent(obj));

            var responseStr = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TK>(responseStr);
        }
    }
}