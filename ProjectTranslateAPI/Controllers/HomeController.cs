using Microsoft.AspNetCore.Mvc;
using ProjectTranslateAPI.Models;
using System.Diagnostics;
using Google.Cloud.Translation.V2;
namespace ProjectTranslateAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var client = TranslationClient.Create();
            TranslationResult result = client.TranslateText("It is raining.", LanguageCodes.Vietnamese);
            var kq = $"Result: {result.TranslatedText}; detected language {result.DetectedSourceLanguage}";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}