using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace bbxp.WebAPI.DataLayer {
    public class RedisFactory {
        private static ConnectionMultiplexer redis;
        private readonly IDatabase db;

        public RedisFactory(string redisConnectionString) {
            if (redis == null) {
                redis = ConnectionMultiplexer.Connect(redisConnectionString);
            }

            if (db == null) {
                db = redis.GetDatabase();
            }
        }

        private string cleanKey(string key) => key.Replace("/", "_");

        public async void WriteJSON<T>(string key, T objectValue) {
            var settings = new JsonSerializerSettings();

            settings.StringEscapeHandling = StringEscapeHandling.Default;

            var value = JsonConvert.SerializeObject(objectValue, Formatting.None, settings);

            value = JToken.Parse(value).ToString();

            await db.StringSetAsync(cleanKey(key), value, flags: CommandFlags.FireAndForget);
        }
    }
}