using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public abstract class KafkaBaseOptions
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string[] BootstrapServers { get; set; }
    }
}
