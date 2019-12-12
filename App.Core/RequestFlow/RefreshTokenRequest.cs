using App.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.RequestFlow
{
    public class RefreshTokenRequest :BaseEntity 
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
