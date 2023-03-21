using Beatus.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Beatus.Services;

public class CustomVisionAIService
{
    private readonly IConfiguration _config;

    public CustomVisionAIService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<Prediction> MakePredictionAsync(byte[] imageBytes)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Prediction-Key", _config["CustomVision:Key"]);

            using var content = new ByteArrayContent(imageBytes);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            var response = await client.PostAsync(_config["CustomVision:EndPoint"], content);
            var responseString = await response.Content.ReadAsStringAsync();
            Prediction prediction = JsonConvert.DeserializeObject<CustomVisionResponse>(responseString).Predictions?.OrderByDescending(x => x.Probability).FirstOrDefault();

            return prediction;
        }

    }
}
