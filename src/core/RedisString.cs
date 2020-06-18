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

        #endregion
    }
}
