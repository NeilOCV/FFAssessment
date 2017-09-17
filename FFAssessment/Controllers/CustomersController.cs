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
        public ActionResult Edit(int id)
        {
            CustomersEntity customer = new CustomersEntity();
            List<CustomersEntity> lst = GetCustomersFromAPI();
            customer = (from tb in lst
                        where tb.id == id
                        select tb).Single();

            return View(customer);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            DELETE(id);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult Edit(int id,CustomersEntity customer)
        {
            PUT(id, customer);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
        private void POST(CustomersEntity customer)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseURL);
            client.PostAsJsonAsync("api/customers",customer);
        }
        private void DELETE(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseURL);
            client.DeleteAsync(strBaseURL + "api/customers/" + id.ToString());
        }
        private void PUT(int id,CustomersEntity customer)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseURL);
            client.PutAsJsonAsync("api/customers/" + id.ToString(), customer);
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