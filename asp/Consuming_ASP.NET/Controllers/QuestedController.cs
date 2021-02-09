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
            if(Consuming_ASP.NET.Models.User.actual_user == null)
            {
                Consuming_ASP.NET.Models.User.actual_user = "Guest";
            }
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

        public ActionResult GameOver(int score, string nickname)
        {
            try
            {
                // TODO: Add insert logic here
                List<HighScore> us;
                us = new List<HighScore>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    //client.BaseAddress = new Uri(ip);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HighScore user = new HighScore(nickname, score);


                    client.BaseAddress = new Uri(ip);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<HighScore>("api/score", user);
                    postTask.Wait();
                    var Res = postTask.Result;
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return View(score);
                    }
                }
            }
            catch
            {
            }

            return RedirectToAction("../Home/Index");
        }

        public ActionResult LogOut()
        {
            Consuming_ASP.NET.Models.User.actual_user = "Guest";
            Consuming_ASP.NET.Models.User.logged = 0;
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> QuestionBoolean(int question_id, int score)
        {
            Questions question = new Questions();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ip);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/question/" + question_id);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var QuestionResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    question = JsonConvert.DeserializeObject<Questions>(QuestionResponse);
                }
                question.act_score = score;
                //returning the employee list to view
                return View(question);
            }
            
        }

        public async Task<ActionResult> QuestionMultiple(int question_id, int score)
        {
            Questions question = new Questions();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ip);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/question/" + question_id);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var QuestionResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    question = JsonConvert.DeserializeObject<Questions>(QuestionResponse);
                }
                question.act_score = score;
                //returning the employee list to view
                return View(question);
            }
        }

        // POST: Quested/Register
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [System.Web.Mvc.HttpPost]
        public ActionResult Register(System.Web.Mvc.FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                List<User> us;
                us = new List<User>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    //client.BaseAddress = new Uri(ip);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    String nickname = collection["nickname"];
                    String password = collection["password"];
                    String email = collection["email"];
                    String name = collection["name"];
                    String surname = collection["surname"];

                    User user = new User(nickname, password, email, name, surname, "player", "0");
                    //question.incorrect_answers = collection["incorrect_answers"].ToString().Split(',');
                    //question.available = collection["true"];

                    client.BaseAddress = new Uri(ip);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<User>("api/user", user);
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

        public async Task<ActionResult> Question(int score)
        {
            var random = new Random();
            int randomnumber = random.Next(1, 1700);

            Questions question = new Questions();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ip);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/question/" + randomnumber);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var QuestionResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    question = JsonConvert.DeserializeObject<Questions>(QuestionResponse);
                }
                //returning the employee list to view
                if (question.type.Equals("boolean")){
                    return RedirectToAction("QuestionBoolean", "Quested", new { question_id = question.question_id, score = score });
                } else
                {
                    return RedirectToAction("QuestionMultiple", "Quested", new { question_id = question.question_id, score = score });
                }
            }
        }

        public async Task<ActionResult> Checked(string erantzuna, string erantzun_zuzena, int score) 
        {
            if (erantzun_zuzena.Equals(erantzuna))
            {
                Models.Questions.actualScore = Models.Questions.actualScore + score;
                return RedirectToAction("Question", "Quested", new { @score = Models.Questions.actualScore });
            }
            else
            {
                score = Models.Questions.actualScore;
                Models.Questions.actualScore = 0;
                return RedirectToAction("GameOver", "Quested", new { @score = score, @nickname = Models.User.actual_user });
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> Login(System.Web.Mvc.FormCollection collection)
        {
            List<User> users = new List<User>();
            int exists = 0;
            using (var client = new HttpClient())
            {
                string nickname = collection["nickname"];
                string password = collection["password"];
                //Passing service base url
                client.BaseAddress = new Uri(ip);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/users");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    users = JsonConvert.DeserializeObject<List<User>>(UserResponse);

                    int i = 0;
                    while (exists == 0 && i < users.Count)
                    {
                        if (users[i].nickname.Equals(nickname))
                        {
                            if (users[i].password.Equals(password))
                            {
                                if (users[i].role.Equals("Administrator"))
                                {
                                    exists = 2;
                                }
                                else if (users[i].role.Equals("player"))
                                {
                                    exists = 1;
                                }
                                
                            }
                            else
                            {
                                i++;
                            }
                        }
                        else
                        {
                            i++;
                        }
                    }
                    if (exists == 1)
                    {
                        Consuming_ASP.NET.Models.User.logged = 1;
                        Consuming_ASP.NET.Models.User.actual_user = nickname;
                        return RedirectToAction("../Quested/Index");
                    }
                    else if (exists == 2)
                    {
                        Consuming_ASP.NET.Models.User.logged = 2;
                        Consuming_ASP.NET.Models.User.actual_user = nickname;
                        return RedirectToAction("../Quested/Index");
                        
                    } else
                    {
                        Consuming_ASP.NET.Models.User.actual_user = "Guest";
                        return RedirectToAction("../Quested/LoginForm");
                    }


                }

                else
                {
                    return RedirectToAction("../Quested/LoginForm");
                }
            }
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
                        incorrect_answers_[0] = collection["incorrect_answers[0]"];
                        if (collection["incorrect_answers[1]"] != "")
                        {
                            incorrect_answers_[1] = collection["incorrect_answers[1]"];
                        }
                        if (collection["incorrect_answers[2]"] != "")
                        {
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
                        return RedirectToAction("../Quested/Index");
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

        public async System.Threading.Tasks.Task<ActionResult> IkusGalderak()
        {
            List<Questions> pList = new List<Questions>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(ip);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/questions");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    pList = JsonConvert.DeserializeObject<List<Questions>>(EmpResponse);
                }
                return View(pList);
            }
        }
        public async System.Threading.Tasks.Task<ActionResult> CreateForm()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> Register()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> LoginForm()
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
