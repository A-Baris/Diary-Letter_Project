using LetterApp.WEB.Models;
using LetterApp.WEB.Models.View_Models;
using LetterApp.WEB.Models.View_Models.DiaryVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace LetterApp.WEB.Controllers
{

    public class DiaryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;

        public DiaryController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _httpClient = _clientFactory.CreateClient();
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var apiUrl = $"https://localhost:7223/api/user/identity/{userId}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserVM>(jsonString);
                var apiUrl2 = $"https://localhost:7223/api/diary/page/{data.Id}";
                var response2 = await _httpClient.GetAsync(apiUrl2);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString2 = await response2.Content.ReadAsStringAsync();
                    var data2 = JsonConvert.DeserializeObject<List<DiaryVM>>(jsonString2);
                    return View(data2);
                }

            }
           
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var apiUrl = $"https://localhost:7223/api/diary/detail/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<DiaryWithNoteVM>>(jsonString);
            

                ViewBag.Id = id;
                return View(data);
            }
            return View(null);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var apiUrl = $"https://localhost:7223/api/user/identity/{userId}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserVM>(jsonString);
                ViewBag.UserId = data.Id;
               
            }
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DiaryCreateVM diaryVM)
        {
            try
            {
                var apiUrl = $"https://localhost:7223/api/diary/create";
                var json = JsonConvert.SerializeObject(diaryVM);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var apiData = JsonConvert.DeserializeObject<DiaryCreateVM>(data);
                    TempData["success"] = "Günlük başarılı şekilde oluşturuldu.";
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


                    return View(diaryVM);

                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to register. Please try again later.";
                    return View("create");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View("create");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var apiUrl = $"https://localhost:7223/api/diary/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DiaryEditVM>(jsonString);

                return View(data);


            }
            return View("index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DiaryEditVM diaryEditVM)
        {
            try
            {
                var apiUrl = "https://localhost:7223/api/diary/edit";
                var json = JsonConvert.SerializeObject(diaryEditVM);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var apiData = JsonConvert.DeserializeObject<DiaryVM>(data);
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


                    return View(diaryEditVM);

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
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var apiUrld = $"https://localhost:7223/api/diary/delete/{id}";
            var response = await _httpClient.DeleteAsync(apiUrld);
            if (response.IsSuccessStatusCode)
            {
                return View(response);
            }
            
            return View(response);
        }

    }
}
