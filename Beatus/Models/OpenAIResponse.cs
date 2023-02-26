namespace Beatus.Models;

public class Choice
{
    public string Text { get; set; }
    public int Index { get; set; }
    public object Logprobs { get; set; }
    public string FinishReason { get; set; }
}

public class OpenAIResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public int Created { get; set; }
    public string Model { get; set; }
    public List<Choice> Choices { get; set; }
    public Usage Usage { get; set; }
}

public class Usage
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
}

