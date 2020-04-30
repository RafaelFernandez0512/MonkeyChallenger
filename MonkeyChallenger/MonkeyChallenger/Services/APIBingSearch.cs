using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using MonkeyCache.FileStore;
using MonkeyChallenger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyChallenger.Services
{
    public class APIBingSearch : IApiBingSearch
    {
        private readonly Random random = new Random();
        public APIBingSearch()
        {
            Barrel.ApplicationId = ConfigApi.CacheChallenger;

        }
        public async Task<List<Picture>> GetBingPicture()
        {
            if (!Barrel.Current.IsExpired(ConfigApi.DestinationKey))
            {
                return Barrel.Current.Get<List<Picture>>(ConfigApi.DestinationKey);
            }

            string[] searchDestinations = new string[] { "Seattle", "Maui", "Nueva York", "Antarctica", "República Dominicana" };
            var clientImage = new ImageSearchClient(new ApiKeyServiceClientCredentials( ConfigApi.BingImageSearch));
            List<Picture> resultDestination = new List<Picture>();
            try
            {
                foreach (var item in searchDestinations)
                {
                    var result = await clientImage.Images.SearchAsync(item, color: "blue", minWidth: 500, minHeight: 500, imageType: "Photo", license: "Public", count: 5, maxHeight: 1200, maxWidth: 1200);
                    int randomIndex = random.Next(result.Value.Count);
                    resultDestination.Add(new Picture
                    {
                        Title = item,
                        Image = result.Value[randomIndex].ContentUrl,
                        Uri = result.Value[randomIndex].HostPageUrl
                    });
                }
                Barrel.Current.Add(ConfigApi.DestinationKey, data: resultDestination, TimeSpan.FromDays(60));
                return resultDestination;
            }
            catch (Exception)
            {

                return resultDestination;
            }
           
        }
    }
}
