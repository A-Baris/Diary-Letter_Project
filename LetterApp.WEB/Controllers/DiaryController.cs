using LetterApp.WEB.Models;
using LetterApp.WEB.Models.View_Models;
using LetterApp.WEB.Models.View_Models.DiaryVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
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
            var apiUrl = "https://localhost:7223/api/diary";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<DiaryVM>>(jsonString);
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var apiUrl = $"https://localhost:7223/api/diary/{id}";
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
        public IActionResult Create()
        {
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
                    TempData["success"] = "Kayıt başarılı şekilde gerçekleşti.";
                    return RedirectToAction("Create");
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

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPut]
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
                    return RedirectToAction("edit",apiData);
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
        [HttpDelete]
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
