using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using UI.Areas.Web.Models;
using Microsoft.AspNetCore.Http;
using UI.Helpers;

namespace UI.Areas.Web.Controllers
{
    [Area("Web")]
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Lütfen tüm alanları doldurun.";
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5117/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var tokenObj = JsonDocument.Parse(json).RootElement;

                var token = tokenObj.GetProperty("token").GetString();

                // 🔍 Kullanıcı adını token içinden al
                var userName = JwtHelper.GetUserNameFromToken(token);

                // 🔐 Token ve kullanıcı adını session'a kaydet
                HttpContext.Session.SetString("token", token);
                HttpContext.Session.SetString("username", userName ?? "Misafir");

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Giriş başarısız. Bilgileri kontrol edin.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Tüm oturum verilerini temizle
            HttpContext.Session.Clear();

            // Giriş sayfasına yönlendir
            return RedirectToAction("Login", "Account", new { area = "Web" });
        }
    }
}
