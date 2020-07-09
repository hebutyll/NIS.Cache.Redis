using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.Cache.Redis
{
    /// <summary>
    /// String对象封装
    /// </summary>
    public class RedisString : BaseRedisObject
    {
        #region 公共方法

        /// <summary>
        /// 根据Key获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return this.Client.db.StringGet(key);
        }

        /// <summary>
        /// 根据Key获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var value = this.Client.db.StringGet(key);
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 根据Key获取字节数组
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] GetBytes(string key)
        {
            var result = this.Client.db.StringGetLease(key);
            if (result == null)
            {
                return null;
            }
            return result.ArraySegment.ToArray();
        }

        /// <summary>
        /// 根据key列表获取对应的字符串列表
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string[] Get(string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return null;
            }
            RedisKey[] redisKeys = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                redisKeys[i] = keys[i];
            }
            return this.Client.db.StringGet(redisKeys)?.ToStringArray();
        }

        /// <summary>
        /// 为给定的key设置value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, string value)
        {
            return this.Client.db.StringSet(key, value);
        }

        /// <summary>
        /// 为给定的key设置value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T obj)
        {
            string value = JsonConvert.SerializeObject(obj);
            return this.Set(key, value);
        }

        /// <summary>
        /// 当key不存在的时候为key设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetNx(string key, string value)
        {
            return this.Client.db.StringSet(key, value, when: When.NotExists);
        }

        /// <summary>
        /// 当key不存在的时候为key设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool SetNx<T>(string key, T obj)
        {
            string value = JsonConvert.SerializeObject(obj);
            return this.SetNx(key, value);
        }

        /// <summary>
        /// 批量设置字典项
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public bool Set(Dictionary<string, string> dic)
        {
            List<KeyValuePair<RedisKey, RedisValue>> list = new List<KeyValuePair<RedisKey, RedisValue>>();
            foreach (var pair in dic)
            {
                list.Add(new KeyValuePair<RedisKey, RedisValue>(pair.Key, pair.Value));
            }
            return this.Client.db.StringSet(list.ToArray());
        }

        /// <summary>
        /// 获取字符串值的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetLenght(string key)
        {
            return this.Client.db.StringLength(key);
        }

        /// <summary>
        /// 获取字符串值指定索引范围上的内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public string GetRange(string key, int startIndex, int endIndex)
        {
            return (string)this.Client.db.StringGetRange(key, startIndex, endIndex);
        }

        /// <summary>
        /// 对字符串值的索引范围进行设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="replace"></param>
        public void SetRange(string key, int startIndex, string replace)
        {
            this.Client.db.StringSetRange(key, startIndex, replace);
        }

        /// <summary>
        /// 追加新内容到值的末尾
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Append(string key, string value)
        {
            return this.Client.db.StringAppend(key, value);
        }

        /// <summary>
        /// 对整数值执行加法操作
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public long Incr(string key, long increment)
        {
            return this.Client.db.StringIncrement(key, increment);
        }

        /// <summary>
        /// 对整数执行加1操作
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long Incr(string key)
        {
            return this.Incr(key, 1);
        }

        /// <summary>
        /// 对整数执行减法操作
        /// </summary>
        /// <param name="key"></param>
        /// <param name="decrement"></param>
        /// <returns></returns>
        public long Decr(string key, long decrement)
        {
            return this.Incr(key, -decrement);
        }

        /// <summary>
        /// 对整数执行减1操作
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long Desr(string key)
        {
            return this.Decr(key, -1);
        }

        /// <summary>
        /// 对数字执行浮点数加法运算
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public double IncyByFloat(string key, double increment)
        {
            return this.Client.db.StringIncrement(key, increment);
        }

        #endregion
    }
}
