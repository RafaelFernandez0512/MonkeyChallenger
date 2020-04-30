using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using MonkeyChallenger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyChallenger.Services
{
    public class APIComputerVision : IAPIComputerVision
    {
        private readonly ComputerVisionClient computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(ConfigApi.ComputerVisionApiKey)) {
            Endpoint = ConfigApi.ComputerVisionEndpoint
        };
        public async Task<AnalyzePicture> AnalyzePicture(Stream image)
        {
            try
            {
                var result = await computerVisionClient.AnalyzeImageInStreamAsync(image, details: new[] { Details.Landmarks }, visualFeatures: new[] { VisualFeatureTypes.Description, VisualFeatureTypes.Color });
                var description = result.Description.Captions.OrderByDescending(d => d.Confidence).FirstOrDefault()?.Text ?? "nothing! No description found";
                var color = Color.FromHex($"{result.Color.AccentColor}");
                var landmark = result.Categories.FirstOrDefault(c => c.Detail != null && c.Detail.Landmarks.Any());
                var landmarkDescription = "";
                landmarkDescription = landmark != null ? landmark.Detail.Landmarks.OrderByDescending(l => l.Confidence).First().Name : "";
                return new AnalyzePicture(description, color, landmarkDescription);
            }
            catch (Exception)
            {

                return new AnalyzePicture(null,Color.Accent,null);
            }
     

        }
    }
}
