using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth20_V1.SettingMappers
{
    public class RedisCacheSettings
    {
        public bool Enabled { get; set; }

        public string ConnectionString { get; set; }
    }
}
