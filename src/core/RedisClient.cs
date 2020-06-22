using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.Cache.Redis
{
    public class RedisClient
    {
        private ConnectionMultiplexer redis = null;
        internal IDatabase db = null;
        internal RedisClient(ConnectionMultiplexer redis)
        {
            this.redis = redis;
            this.db = redis.GetDatabase();
        }

        #region 公共方法

        /// <summary>
        /// 设置要操作的数据库
        /// </summary>
        /// <param name="number"></param>
        public void SetDataBase(int number)
        {
            this.db = redis.GetDatabase(number);
        }

        /// <summary>
        /// 创建RedisObject对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateRedisObject<T>() where T : BaseRedisObject
        {
            var obj = default(T);
            obj.Client = this;
            return obj;
        }

        /// <summary>
        /// 判断数据库中是否存在给定的键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ExistsLey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            return this.db.KeyExists(key);
        }
        #endregion
    }
}
