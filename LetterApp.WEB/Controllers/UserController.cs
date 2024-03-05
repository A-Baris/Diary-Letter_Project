
using LetterApp.WEB.IdentityContext.IdentityEntities;
using LetterApp.WEB.Models;
using LetterApp.WEB.Models.View_Models;
using LetterApp.WEB.Models.View_Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

using System.Text;

namespace LetterApp.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly HttpClient _httpClient;

        public UserController(IHttpClientFactory httpClientFactory,UserManager<AppUser> userManager)
        {
            _httpClientFactory = httpClientFactory;
           _userManager = userManager;
            _httpClient = _httpClientFactory.CreateClient();
        }
        [HttpGet]
        public async Task<IActionResult> News()
        
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Add("authorization", "apikey 4ZPifDVQ0Edn8K2OyT5GrE:5GulT9Y4t7vHEdeDaRY6Oe");

                var response = await client.GetAsync("https://api.collectapi.com/news/getNews?country=tr&tag=general");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(jsonString);

                    return View(apiResponse.Result);
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
          

            string apiUrl = "https://localhost:7223/api/user";
            var response = await _httpClient.GetAsync(apiUrl);
            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<UserVM>>(jsonString);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            
            string apiUrl = $"https://localhost:7223/api/user/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<UserRegister>(jsonString);
            return View(data);
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {

            if (ModelState.IsValid)
            {
                var newUser = new AppUser
                {
                    UserName = userRegister.Username,
                    Email = userRegister.Email,

                };
                var result = await _userManager.CreateAsync(newUser, userRegister.Password);
                if (result.Succeeded)
                {
                    var user = new UserCreateVM { IdentityId=newUser.Id, Username= newUser.UserName, Email=newUser.Email,Password=newUser.PasswordHash };
                    try
                    {


                        string apiUrl = "https://localhost:7223/api/user";


                        string json = JsonConvert.SerializeObject(user);
                        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);


                        if (response.IsSuccessStatusCode)
                        {
                            string data = await response.Content.ReadAsStringAsync();
                            var apiData = JsonConvert.DeserializeObject<UserCreateVM>(data);
                            TempData["success"] = "Kayıt başarılı şekilde gerçekleşti.";
                            return RedirectToAction("Register");
                        }
                        else if (response.StatusCode == HttpStatusCode.BadRequest)
                        {

                            string errorContent = await response.Content.ReadAsStringAsync();
                            var apiErrorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(errorContent);


                            foreach (var error in apiErrorResponse.Errors)
                            {
                                foreach (var errorMessage in error.Value)
                                {

                                    ModelState.AddModelError(error.Key, errorMessage);
                                }
                            }


                            return View(userRegister);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Failed to register. Please try again later.";
                            return View("Register");
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                        return View("Register");
                    }
                }
            }


            try
            {


                string apiUrl = "https://localhost:7223/api/user";


                string json = JsonConvert.SerializeObject(userRegister);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);


                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var apiData = JsonConvert.DeserializeObject<UserRegister>(data);
                    TempData["success"] = "Kayıt başarılı şekilde gerçekleşti.";
                    return RedirectToAction("Register");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {

                    string errorContent = await response.Content.ReadAsStringAsync();
                    var apiErrorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(errorContent);


                    foreach (var error in apiErrorResponse.Errors)
                    {
                        foreach (var errorMessage in error.Value)
                        {

                            ModelState.AddModelError(error.Key, errorMessage);
                        }
                    }


                    return View(userRegister);
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to register. Please try again later.";
                    return View("Register");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View("Register");
            }
        }
        [HttpGet]
        public async  Task<IActionResult> Edit(string id)
        {

            try
            {
           
                string apiUrl = $"https://localhost:7223/api/user/{id}";
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); 

                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserUpdate>(jsonString);

                return View("edit",data);
            }
            catch (HttpRequestException ex)
            {
             
                Console.WriteLine($"Error fetching user with ID {id}: {ex.Message}");
                return StatusCode(500); 
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdate userUpdate)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userUpdate.UserId);
                if (user == null) return View();
                user.UserName = userUpdate.Username;
                    user.Email=userUpdate.Email;
              
            }
            try
            {
              

                string apiUrl = $"https://localhost:7223/api/user/update";


                string json = JsonConvert.SerializeObject(userUpdate);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, content);


                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var apiData = JsonConvert.DeserializeObject<UserUpdate>(data);
                    TempData["success"] = "Güncelleme başarılı şekilde gerçekleşti.";
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {

                    string errorContent = await response.Content.ReadAsStringAsync();
                    var apiErrorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(errorContent);


                    foreach (var error in apiErrorResponse.Errors)
                    {
                        foreach (var errorMessage in error.Value)
                        {

                            ModelState.AddModelError(error.Key, errorMessage);
                        }
                    }


                    return View(userUpdate);
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to register. Please try again later.";
                    return View("edit");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View("edit");
            }
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string apiUrl = $"https://localhost:7223/api/user/delete/{id}";
                var response = await _httpClient.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = $"Id: {id} Silme İşlemi Başarılı";
                }
               else
                {
                    TempData["error"] = $"Id: {id} Silme İşlemi Başarısız";
                }

                return RedirectToAction("index",response);

            }
            catch
            {
                throw;
            }

        }
    }
   
}

