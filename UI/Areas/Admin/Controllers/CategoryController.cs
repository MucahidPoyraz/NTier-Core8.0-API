using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using UI.Models.CategoryModels;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string apiUrl = "http://localhost:5117/api/v1/Categories";
            List<CategoryModel> categories = new List<CategoryModel>(); // Boş bir liste oluşturduk

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<CategoryModel>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    ViewBag.ErrorMessage = "API isteği başarısız!";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Hata oluştu: {ex.Message}";
            }

            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryModel model)
        {
            string apiUrl = "http://localhost:5117/api/v1/Categories";

            try
            {
                string jsonData = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
                var msj = response.Content.ReadAsStringAsync();
                var resutl = msj.Result;
                
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Kategori başarıyla eklendi!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = $"API isteği başarısız! Hata kodu: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = $"Hata oluştu: {ex.Message}";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            string apiUrl = $"http://localhost:5117/api/v1/Categories/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                UpdateCategoryModel result = JsonSerializer.Deserialize<UpdateCategoryModel>(jsonData,new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryModel model)
        {
            string apiUrl = $"http://localhost:5117/api/v1/Categories/{model.Id}";

            try
            {
                string jsonData = JsonSerializer.Serialize(model);
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage result = await _httpClient.PutAsync(apiUrl, content);

                string responseContent = await result.Content.ReadAsStringAsync(); // Hata mesajını oku

                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Kategori başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = $"API hatası: {result.StatusCode} - {responseContent}";
                }
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = $"Bağlantı hatası: {ex.Message}";
            }

            return View(model);
        }

    }
}
