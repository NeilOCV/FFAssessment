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
        public ActionResult EditContact(int id)
        {
            ContactEntity _contact = new ContactEntity();
            //List<ContactEntity> lst = new List<ContactEntity>();
            _contact = GetContactFromAPI(id);
            
            return View(_contact);
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
        [HttpGet]
        public ActionResult Contacts(int id)
        {
            List<ContactEntity> _contacts = new List<ContactEntity>();

            _contacts = GetContactsForCustomerFromAPI(id);

            return View(_contacts);
        }
        [HttpGet]
        public ActionResult DeleteContact(int id)
        {
            DELETEContact(id);
            return RedirectToAction("Index");
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
        private void DELETEContact(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseURL);
            client.DeleteAsync(strBaseURL + "api/contacts/" + id.ToString());
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
        private ContactEntity GetContactFromAPI(int contactID)
        {
            List<ContactEntity> contacts = GetContactsForCustomerFromAPI(-1);
            ContactEntity contact = new ContactEntity();
            if (contacts.Count > 0)
            {
                var prog = (from tb in contacts
                           where tb.id == contactID
                           select tb).Single();
                if (prog != null)
                    contact = prog;

            }
            return contact;
        }
        private List<ContactEntity> GetContactsForCustomerFromAPI(int customerID)
        {
            string strURIModifier = "";
            if (customerID > -1)
            {
                strURIModifier = "?cust_id=" + customerID.ToString();
            }
            try
            {
                var resultList = new List<ContactEntity>();
                var client = new HttpClient();
                var getDataTask = client.GetAsync(strBaseURL + "api/contacts" + strURIModifier)
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