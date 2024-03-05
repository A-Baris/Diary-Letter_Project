using LetterApp.WEB.Models;
using LetterApp.WEB.Models.View_Models;
using LetterApp.WEB.Models.View_Models.DiaryNoteVM;
using LetterApp.WEB.Models.View_Models.DiaryVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using static System.Net.WebRequestMethods;

namespace LetterApp.WEB.Controllers
{
    public class DiaryNoteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;

        public DiaryNoteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var apiUrl = "https://localhost:7223/api/diary";
            var response = await _client.GetAsync(apiUrl);
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
            var apiUrl = $"https://localhost:7223/api/diarynote/{id}";
            var response = await _client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DiaryNoteDetailVM>(jsonString);
                return View(data);
            }
            return RedirectToAction("index","diary");
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.Id= id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiaryNoteCreateVM noteCreateVm)
        {
            try
            {
            var apiUrl = "https://localhost:7223/api/diarynote/create";
            var json = JsonConvert.SerializeObject(noteCreateVm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var apiData = JsonConvert.DeserializeObject<DiaryNoteCreateVM>(data);
             
                ViewBag.Id = apiData.DiaryId;
                    return RedirectToAction("Details", "Diary", new { id = apiData.DiaryId });
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


                    return View(noteCreateVm);

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

            var apiUrl = $"https://localhost:7223/api/diarynote/{id}";
            var response = await _client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DiaryNoteDetailVM>(jsonString);
                return View(data);
            }
            return View("index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DiaryNoteDetailVM editVm)
        {

            try
            {
                var apiUrl = "https://localhost:7223/api/diarynote/edit";
                var json = JsonConvert.SerializeObject(editVm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var apiData = JsonConvert.DeserializeObject<DiaryNoteDetailVM>(data);
                    TempData["success"] = "Kayıt başarılı şekilde gerçekleşti.";
                    ViewBag.Id = apiData.DiaryId;
                    return RedirectToAction("Details", "Diary", new { id = apiData.DiaryId });
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


                    return View(editVm);

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
            var apiUrl = $"https://localhost:7223/api/diarynote/delete/{id}";
            var response = await _client.DeleteAsync(apiUrl);
            if(response.IsSuccessStatusCode)
            {
              return RedirectToAction("Index", "Diary");
            }
            return View();
        }

    }
}
