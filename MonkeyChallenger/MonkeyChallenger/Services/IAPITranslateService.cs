using MonkeyChallenger.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyChallenger.Services
{
    [Headers("Content-Type: application/json")]
    public interface IAPITranslateService
    {
        [Post("/translate?api-version=3.0&to=en&to=nl&to=es&to=fr")]
        Task<TranslationResult[]> TranslateText([Body]string text,[Header("Ocp-Apim-Subscription-Key")]string apikey);

    }
}   
        