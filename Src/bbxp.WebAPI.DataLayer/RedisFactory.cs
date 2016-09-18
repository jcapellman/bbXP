using Newtonsoft.Json;

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

        public async void WriteJSON<T>(string key, T objectValue) {
            var value = JsonConvert.SerializeObject(objectValue);

            await db.StringSetAsync(key, value, flags: CommandFlags.FireAndForget);
        }
    }
}