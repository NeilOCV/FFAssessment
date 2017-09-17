using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace APIMessages
{
    public class CustomersEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        [DisplayFormat(DataFormatString = "{0:##0.000000000}", ApplyFormatInEditMode = true)]
        public decimal latitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:##0.000000000}", ApplyFormatInEditMode = true)]
        public decimal longetude { get; set; }
    }
}