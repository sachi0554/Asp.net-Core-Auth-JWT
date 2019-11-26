using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.ResponseFlow
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
