using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ProjectTranslateAPI.ViewModels;
using ProjectTranslateAPI.Model;

namespace ProjectTranslateAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GoogleTranslateHelper _googleTranslateHelper;
        public HomeController(ILogger<HomeController> logger, GoogleTranslateHelper googleTranslateHelper)
        {
            _logger = logger;
            _googleTranslateHelper = googleTranslateHelper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ResultObject Tranlation([FromBody] TranslationModel model)
        {
            ResultObject resultObject = new ResultObject() { Ok = false};
            if(model != null)
            {
                //System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @".\Resource\projecttranslateapi-397007-4c3ee3daa0e2.json");
                //var client = TranslationClient.Create();
                //if (model.Language == "VietNamese")
                //{
                //    TranslationResult result = client.TranslateText(model.Text, LanguageCodes.English);
                //    resultObject.Ok = true;
                //    resultObject.Content = result.TranslatedText;
                //    return resultObject;
                //}
                //else if(model.Language == "English")
                //{
                //    TranslationResult result = client.TranslateText(model.Text, LanguageCodes.Vietnamese);
                //    resultObject.Ok = true;
                //    resultObject.Content = result.TranslatedText;
                //    return resultObject;
                //}
                return _googleTranslateHelper.LanguageTranslation(model);
            }
            return resultObject;
        }
    }
}