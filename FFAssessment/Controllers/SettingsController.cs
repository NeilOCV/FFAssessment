using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace FFAssessment.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        [HttpGet]
        public ActionResult Index()
        {
            List<Settings> lst = ReadSettingsFromConfig();
            return View(lst);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Settings setting = new Settings();
            List<Settings> lst = ReadSettingsFromConfig();
            if (lst.Count() > 0)
                setting = lst[0];


            return View(setting);
        }
        [HttpPost]
        public ActionResult Edit(Settings setting)
        {
            Utillities.ConfigFiles config = new Utillities.ConfigFiles();
            config.SaveValue("APIURL", setting.URL);
            
            return View("edit");
        }
        private List<Settings> ReadSettingsFromConfig()
        {
            List<Settings> lst = new List<Settings>();

            Utillities.ConfigFiles config = new Utillities.ConfigFiles();
            string strURL = config.GetValue("APIURL");
            Settings setting = new Settings();
            setting.URL = strURL;
            lst.Add(setting);

            return lst;
        }
    }
}