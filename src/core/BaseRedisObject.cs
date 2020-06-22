using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.Cache.Redis
{
    public abstract class BaseRedisObject
    {
        internal RedisClient Client { get; set; }

        #region 设置键过期

        /// <summary>
        /// 为键设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool Expire(string key, DateTime expireTime)
        {
            TimeSpan span = expireTime - DateTime.Now;
            return this.Client.db.KeyExpire(key, span);
        }

        /// <summary>
        /// 为键设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expireSpan"></param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan expireSpan)
        {
            return this.Client.db.KeyExpire(key, expireSpan);
        }

        #endregion
    }
}
