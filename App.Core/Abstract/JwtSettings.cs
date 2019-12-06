using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Abstract
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { set; get; }
        public string Audience { set; get; }
        public TimeSpan TokenLifetime { get; set; }
    }
}
    