using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Error
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
