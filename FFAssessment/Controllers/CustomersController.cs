using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIMessages;
using System.Net.Http;

namespace FFAssessment.Controllers
{
    public class CustomersController : Controller
    {
        const string strBaseURL = "http://localhost:20564/";
        // GET: Customers
        public ActionResult Index()
        {
            var customers = GetCustomersFromAPI();

            return View(customers);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(CustomersEntity customer)
        {
            POST(customer);
            return View("Create");
        }
        private void POST(CustomersEntity customer)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseURL);
            client.PostAsJsonAsync("api/customers",customer);
        }
        private List<CustomersEntity> GetCustomersFromAPI()
        {
            try
            {
                var resultList = new List<CustomersEntity>();
                var client = new HttpClient();
                var getDataTask = client.GetAsync(strBaseURL + "api/Customers")
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if(result.StatusCode==System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<CustomersEntity>>();
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