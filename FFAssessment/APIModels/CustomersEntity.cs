using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIMessages
{
    public class CustomersEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal latitude { get; set; }
        public decimal longetude { get; set; }
    }
}