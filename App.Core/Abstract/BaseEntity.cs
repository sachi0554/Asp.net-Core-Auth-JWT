using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Abstract
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

    }
}
