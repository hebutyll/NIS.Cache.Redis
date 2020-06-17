using StackExchange.Redis;
using System;

namespace NIS.Cache.Redis
{
    public class RedisClientFactory
    {
        private static ConnectionMultiplexer redis = null;

        private static object lockObj = new object();

        /// <summary>
        /// 根据配置对象创建客户端连接对象
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static RedisClient CreateClient(RedisOptions options)
        {
            if (redis == null)
            {
                lock (lockObj)
                {
                    redis = ConnectionMultiplexer.Connect(options.ToString());
                }
            }
            return new RedisClient(redis);

        }
    }
}
