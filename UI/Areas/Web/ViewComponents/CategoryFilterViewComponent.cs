using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using UI.Models.CategoryModels;
using UI.Models.PaginationModels;

namespace UI.ViewComponents
{
    public class CategoryFilterViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryFilterViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5117/api/v1/Categories");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<CategoryModel>()); // hata durumunda boş liste
            }

            var json = await response.Content.ReadAsStringAsync();

            // Doğru tipe deserialize et
            var categories = JsonSerializer.Deserialize<PaginatedResponse<CategoryModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(categories.Items);
        }
    }
}
