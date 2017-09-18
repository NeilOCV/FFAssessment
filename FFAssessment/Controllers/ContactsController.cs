using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIMessages;
using System.Net.Http;

namespace FFAssessment.Controllers
{
    public class ContactsController : Controller
    {
        string strBaseURL = string.Empty;
        private string GetBaseURL()
        {
            Utillities.ConfigFiles config = new Utillities.ConfigFiles();
            strBaseURL = config.GetValue("APIURL");
            return strBaseURL;
        }
        //const string strBaseURL = "http://localhost:20564/";
        // GET: Contacts
        public ActionResult Index()
        {
            var contacts = GetContactsFromAPI();

            return View(contacts);
        }
        private List<ContactEntity> GetContactsFromAPI()
        {
            try
            {
                var resultList = new List<ContactEntity>();
                var client = new HttpClient();
                var getDataTask = client.GetAsync(GetBaseURL() + "api/Contacts")
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<ContactEntity>>();
                            readResult.Wait();
                            resultList = readResult.Result;
                        }
                    });
                getDataTask.Wait();
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}