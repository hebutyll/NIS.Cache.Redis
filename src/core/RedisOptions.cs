using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.Cache.Redis
{
    public class RedisOptions
    {
        /// <summary>
        /// 连接IP列表
        /// </summary>
        public List<string> ConnectionIP { get; set; }

        /// <summary>
        /// 连接端口列表
        /// </summary>
        public List<int> ConnectionPort { get; set; }

        /// <summary>
        /// 输出连接字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (ConnectionIP == null || ConnectionIP.Count == 0 || ConnectionPort.Count == 0
                || ConnectionIP.Count != ConnectionPort.Count)
            {
                return "localhost:6379";
            }
            List<string> connStr = new List<string>();
            for (int i = 0; i < ConnectionIP.Count; i++)
            {
                connStr.Add($"{ConnectionIP[i]}:{ConnectionPort[i]}");
            }
            return string.Join(',', connStr);
        }
    }
}
