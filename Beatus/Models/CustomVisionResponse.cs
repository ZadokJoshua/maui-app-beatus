namespace Beatus.Models;

public class Prediction
{
    public string TagId { get; set; }
    public string TagName { get; set; }
    public double Probability { get; set; }
}

public class CustomVisionResponse
{
    public string Id { get; set; }
    public string Project { get; set; }
    public string Iteration { get; set; }
    public DateTime Created { get; set; }
    public IList<Prediction> Predictions { get; set; }
}
