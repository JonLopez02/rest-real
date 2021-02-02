using Consuming_ASP.NET.Models;
using Consuming_ASP.NET.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ActionResult = System.Web.Mvc.ActionResult;

namespace Consuming_ASP.NET.Controllers
{
    public class QuestedController : Controller
    {
        private string ip = "http://192.168.72.2:8080";
        // GET: Quested
        private readonly Service _questionsService;
        [System.Web.Mvc.Route("api/questions")]

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            List<Questions> quested = new List<Questions>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(ip);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/quesiton");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    quested = JsonConvert.DeserializeObject<List<Questions>>(EmpResponse);

                }
                return View(quested);
            }
        }

        // GET: Quested/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Register(System.Web.Mvc.FormCollection collection)
        {
            /*var Connection = "mongodb://192.168.72.3";
            var client = new MongoClient(Connection);
            var Db = client.GetDatabase("Demo1");
            var Collection = Db.GetCollection<User>("User");
            User na = new User();
            na.name = collection["name"];
            na.password = collection["password"];

            Collection.InsertOneAsync(na);*/



            var postTask = client.PostAsJsonAsync<Questions>("api/user", na);
            postTask.Wait();
            var Res = postTask.Result;
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            //var credential = MongoCredential.
        }
        public async Task<ActionResult> Question()
        {

            List<Questions> quested = new List<Questions>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(ip);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/quesiton");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    quested = JsonConvert.DeserializeObject<List<Questions>>(EmpResponse);

                }
                return View(quested);
            }
        }


        public ActionResult Login(System.Web.Mvc.FormCollection collection)
        {
            try
            {
                String username = collection["username"];
                String password = collection["password"];
            }
            catch { }
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> Score(List<HighScore> score)
        {
            //List<Score> quested = new List<Score>();
            score = new List<HighScore>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(ip);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/scores/top");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //quested = JsonConvert.DeserializeObject<List<Score>>(EmpResponse);
                    score = JsonConvert.DeserializeObject<List<HighScore>>(EmpResponse);

                }
                return View(score);
            }
        }
        /**/
        // POST: Quested/Create
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(System.Web.Mvc.FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                List<Questions> s;
                s = new List<Questions>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    //client.BaseAddress = new Uri(ip);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    String category_ = collection["category"];
                    String type_ = collection["type"];
                    String difficulty_ = collection["difficulty"];
                    String question_ = collection["question"];
                    String correct_answer_ = collection["correct_answer"];
                    //String[] incorrect_answers = collection["incorrect_answers[0]"].Split(',');
                    String[] incorrect_answers_ = new String[3];

                    try
                    {

                        if (collection["incorrect_answers[1]"] == null && collection["incorrect_answers[2]"] == null)
                        {
                            incorrect_answers_[0] = collection["incorrect_answers[0]"];
                        }
                        else if (collection["incorrect_answers[1]"] == null)
                        {
                            incorrect_answers_[0] = collection["incorrect_answers[0]"];
                        }
                        else if (collection["incorrect_answers[2]"] == null)
                        {
                            incorrect_answers_[0] = collection["incorrect_answers[0]"];
                        }
                        else if (collection["incorrect_answers[0]"] != null && collection["incorrect_answers[1]"] != null && collection["incorrect_answers[2]"] != null)
                        {
                            incorrect_answers_[0] = collection["incorrect_answers[0]"];
                            incorrect_answers_[1] = collection["incorrect_answers[1]"];
                            incorrect_answers_[2] = collection["incorrect_answers[2]"];
                        }
                    }
                    catch (Exception e)
                    {
                    }


                    Questions question = new Questions(category_, type_, difficulty_, question_, correct_answer_, incorrect_answers_, true);
                    //question.incorrect_answers = collection["incorrect_answers"].ToString().Split(',');
                    //question.available = collection["true"];

                    client.BaseAddress = new Uri(ip);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Questions>("api/question", question);
                    postTask.Wait();
                    var Res = postTask.Result;
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    //HttpContent data = new StringContent(questionjson);

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    //await client.PostAsync("api/question", question);
                }
            }
            catch
            {
            }

            return RedirectToAction("../Home/Index");

        }
        public async System.Threading.Tasks.Task<ActionResult> CreateForm()
        {

            return View();
        }



        // GET: Quested/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Quested/Edit/5
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult Edit(int id, System.Web.Mvc.FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Quested/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Quested/Delete/5
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult Delete(int id, System.Web.Mvc.FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
