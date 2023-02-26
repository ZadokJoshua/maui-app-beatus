using Beatus.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Beatus.Services;

public class OpenAiService
{
    private readonly IConfiguration _config;

    public OpenAiService(IConfiguration config)
    {
        _config = config;
    }

    private sealed record RequestData(string model, string prompt, int max_tokens, int temperature, string stop);

    public async Task<string> GetPlantTips(string plantName)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config["OpenAI:Key"]}");

            RequestData requestData = new
            (
                model: "text-davinci-001",
                prompt: $"Please provide tips about caring for a {plantName} plant, including how to treat it if it becomes diseased.",
                max_tokens: 512,
                temperature: 1,
                stop: @"\n"
            );

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{_config["OpenAI:Endpoint"]}", content);

            var responseJson = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<OpenAIResponse>(responseJson);

            return responseData.Choices.FirstOrDefault()?.Text.Trim();
        }
    }


}