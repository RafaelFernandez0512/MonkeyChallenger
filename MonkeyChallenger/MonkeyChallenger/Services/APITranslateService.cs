using MonkeyChallenger.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyChallenger.Services
{
    public class APITranslateService : IAPITranslateService
    {
        public async Task<TranslationResult[]> TranslateText([Body] string text, [Header("Ocp-Apim-Subscription-Key")] string apikey)
        {
            var body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);
            var request = RestService.For<IAPITranslateService>(ConfigApi.TranslationsEndpoint);
            var translate = await request.TranslateText(requestBody, ConfigApi.TranslationsApiKey);
            return translate;
        }
    }
}
