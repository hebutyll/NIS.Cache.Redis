using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.Cache.Redis
{
    public abstract class BaseRedisObject
    {
        internal RedisClient Client { get; set; }
    }
}
