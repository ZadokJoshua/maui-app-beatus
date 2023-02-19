namespace Beatus.Maui.Models;

public class PredictionDetails
{
    public int Id { get; set; } = 1;
    public byte[] PlantImage { get; set; }
    public string TagName { get; set; }
    public int Accuracy { get; set; }
    public string Recommendation { get; set; }

    public PredictionDetails(byte[] plantImage, string tagName, int accuracy, string recommendation)
    {
        PlantImage = plantImage;
        TagName = tagName;
        Accuracy = accuracy;
        Recommendation = recommendation;
    }
}
