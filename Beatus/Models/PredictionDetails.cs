namespace Beatus.Models;

public class PredictionDetails
{
    public byte[] PlantImage { get; set; }
    public string TagName { get; set; }
    public int Probability { get; set; }
    public string Recommendation { get; set; }
}
