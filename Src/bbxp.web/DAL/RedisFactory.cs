using System;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using StackExchange.Redis;

using bbxp.lib.Enums;

namespace bbxp.web.DAL
{
    public class RedisFactory
    {
        private static ConnectionMultiplexer redis;
        private readonly IDatabase db;

        public RedisFactory(string redisConnectionString)
        {
            if (redis == null)
            {
                redis = ConnectionMultiplexer.Connect(redisConnectionString);
            }

            if (db == null)
            {
                db = redis.GetDatabase();
            }
        }

        public async Task<bool> WriteJSONAsync<T>(MainCacheKeys cacheKey, T objectValue)
            => await WriteJSONAsync(cacheKey.ToString(), objectValue);

        public async Task<bool> WriteJSONAsync<T>(string key, T objectValue)
        {
            var value = JsonConvert.SerializeObject(objectValue, Formatting.None);

            value = JToken.Parse(value).ToString();

            return await db.StringSetAsync(Uri.EscapeDataString(key), value, flags: CommandFlags.FireAndForget);
        }
    }
}