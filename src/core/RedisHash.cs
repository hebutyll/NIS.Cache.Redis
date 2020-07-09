using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.Cache.Redis
{
    /// <summary>
    /// 散列Hash对象封装
    /// </summary>
    public class RedisHash : BaseRedisObject
    {
        #region 公共方法

        /// <summary>
        /// 为字段设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, string field, string value)
        {
            return this.Client.db.HashSet(key, field, value);
        }

        /// <summary>
        /// 为不存在的字段设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetNx(string key, string field, string value)
        {
            return this.Client.db.HashSet(key, field, value, StackExchange.Redis.When.NotExists);
        }

        /// <summary>
        /// 一次为多个字段设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        public void Set(string key, Dictionary<string, string> dic)
        {
            List<HashEntry> entryList = new List<HashEntry>();
            foreach (var pair in dic)
            {
                entryList.Add(new HashEntry(pair.Key, pair.Value));
            }
            this.Client.db.HashSet(key, entryList.ToArray());
        }

        /// <summary>
        /// 获取字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string Get(string key, string field)
        {
            return this.Client.db.HashGet(key, field);
        }

        /// <summary>
        /// 获取指定字段的所有键值，结果以字典的方式返回
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> Get(string key)
        {
            var entryList = this.Client.db.HashGetAll(key);
            if (entryList == null)
            {
                return null;
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var entry in entryList)
            {
                result.Add(entry.Name, entry.Value);
            }
            return result;
        }

        /// <summary>
        /// 对字段存储的整数值执行加法或者减法操作
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public long Incr(string key, string field, long increment)
        {
            return this.Client.db.HashIncrement(key, field, increment);
        }

        /// <summary>
        /// 对字段存储的数值执行浮点加法或减法操作
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public double Incr(string key, string field, double increment)
        {
            return this.Client.db.HashIncrement(key, field, increment);
        }

        /// <summary>
        /// 获取字段的字节长度
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public long GetFieldLength(string key, string field)
        {
            return this.Client.db.HashStringLength(key, field);
        }

        /// <summary>
        /// 检查字段是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool ExistField(string key, string field)
        {
            return this.Client.db.HashExists(key, field);
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool DeleteField(string key, string field)
        {
            return this.Client.db.HashDelete(key, field);
        }

        /// <summary>
        /// 获取散列包含的字段数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetLength(string key)
        {
            return this.Client.db.HashLength(key);
        }

        #endregion
    }
}
