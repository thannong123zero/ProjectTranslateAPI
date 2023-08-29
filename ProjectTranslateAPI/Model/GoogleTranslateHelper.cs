using ProjectTranslateAPI.ViewModels;
using Google.Cloud.Translation.V2;
using System.Reflection;
using Newtonsoft.Json;
using System.Text;
using System;
using Newtonsoft.Json.Linq;

namespace ProjectTranslateAPI.Model
{
    public class GoogleTranslateHelper
    {
        public readonly WebAppAPIConfiguration _webAppAPIConfiguration;
        public GoogleTranslateHelper(WebAppAPIConfiguration webAppAPIConfiguration)
        {
            _webAppAPIConfiguration = webAppAPIConfiguration;
        }
        public ResultObject LanguageTranslation(ViewModels.TranslationModel translationModel)
        {
            ResultObject resultObject = new ResultObject() { Ok = false};
            string translationMethod = _webAppAPIConfiguration.GoogleTranslationMethod;
            string rest = _webAppAPIConfiguration.GoogleTranslationREST;
            string googleEnvironmentName = _webAppAPIConfiguration.GoogleEnvironmentName;
            string googleEnvironmentValue = _webAppAPIConfiguration.GoogleEnvironmentValue;
            string googleTranslationUrl = _webAppAPIConfiguration.GoogleTranslationUrl;
            string googleTranslationKey = _webAppAPIConfiguration.GoogleTranslationKey;

            if (string.Equals(translationMethod, rest)){
                return LanguageTranslationByREST(translationModel, googleTranslationUrl, googleTranslationKey);
            }
            else
            {
                return LanguageTranslationByClientLibrary(translationModel, googleEnvironmentName, googleEnvironmentValue);
            }
        }

        private ResultObject LanguageTranslationByClientLibrary(ViewModels.TranslationModel translationModel,string googleEnvironmentName,string googleEnvironmentValue)
        {
            ResultObject resultObject = new ResultObject() { Ok = false};
            if (translationModel != null && !string.IsNullOrEmpty(googleEnvironmentName) && !string.IsNullOrEmpty(googleEnvironmentValue))
            {
                System.Environment.SetEnvironmentVariable(googleEnvironmentName, googleEnvironmentValue);
                var client = TranslationClient.Create();
                if (translationModel.Language == "VietNamese")
                {
                    TranslationResult result = client.TranslateText(translationModel.Text, LanguageCodes.English);
                    resultObject.Ok = true;
                    resultObject.Content = result.TranslatedText;
                    return resultObject;
                }
                else if (translationModel.Language == "English")
                {
                    TranslationResult result = client.TranslateText(translationModel.Text, LanguageCodes.Vietnamese);
                    resultObject.Ok = true;
                    resultObject.Content = result.TranslatedText;
                    return resultObject;
                }
            }
            return resultObject;
        }

        private ResultObject LanguageTranslationByREST(ViewModels.TranslationModel translationModel,string googleTranslationUrl,string googleTranslationKey)
        {
            ResultObject resultObject = new ResultObject() { Ok = false};
            if (translationModel != null && !string.IsNullOrEmpty(googleTranslationUrl) && !string.IsNullOrEmpty(googleTranslationKey))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(googleTranslationUrl + googleTranslationKey);
                    string source = "";
                    string target = "";
                    if(translationModel.Language == "VietNamese")
                    {
                         source = "vi";
                         target = "en";
                    }
                    else
                    {
                        source = "en";
                        target = "vi";
                    }
                    var content = new FormUrlEncodedContent(new[]
                    {
                            new KeyValuePair<string, string>("key", googleTranslationKey),
                            new KeyValuePair<string, string>("q", translationModel.Text),
                            new KeyValuePair<string, string>("source", source),
                            new KeyValuePair<string, string>("target", target),
                            new KeyValuePair<string, string>("format", "text"),
                        });
                    var result = client.PostAsync("", content).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string jsonData = result.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(jsonData))
                        {
                            JObject obj = JObject.Parse(jsonData);
                            string value = obj["data"]["translations"][0]["translatedText"].ToString();
                            resultObject.Ok = result.IsSuccessStatusCode;
                            resultObject.Content = value;
                            return resultObject;
                        }
                    }
                }
            }
            return resultObject;
        }
    }
}
