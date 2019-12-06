using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Error
{
    public class ErrorResponse
    {
        public IList<ErrorModel> Errors { set; get; } = new List<ErrorModel>();
    }
}
