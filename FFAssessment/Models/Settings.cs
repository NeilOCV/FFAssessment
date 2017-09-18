using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;


namespace FFAssessment
{
    public class Settings
    {
        [Key]
        public int id { get; set; }
        public string URL { get; set; }
    }
}