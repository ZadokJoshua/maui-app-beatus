using Beatus.Maui.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Beatus.Maui.Services;

public class CustomVisionAIService
{
    private readonly IConfiguration _config;

    public CustomVisionAIService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<Prediction> MakePredictionAsync(byte[] imageBytes)
    {
        var url = "https://uksouth.api.cognitive.microsoft.com/customvision/v3.0/Prediction/97a78823-f1cb-4685-b40a-6f6b97acb0b9/classify/iterations/PlantDiseases/image";
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Prediction-Key", "c343537808e34c58ac36d2f9a9311a9d");

            using (var content = new ByteArrayContent(imageBytes))
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                if (content != null)
                {
                    var response = await client.PostAsync(url, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    Prediction prediction = (JsonConvert.DeserializeObject<CustomVisionResponse>(responseString)).Predictions?.OrderByDescending(x => x.Probability).FirstOrDefault();

                    return prediction;
                }
                else
                {
                    Console.WriteLine("Failed");
                    return null;
                }
            }
        }
        
    }
}
