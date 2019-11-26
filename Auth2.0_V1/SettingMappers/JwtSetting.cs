using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth20_V1.SettingMappers
{
    public class JwtSetting
    {
        public string Secret { get; set; }

         public TimeSpan TokenLifetime { get; set; }
    }
}
