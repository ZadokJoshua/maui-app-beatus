using SQLite;

namespace Beatus.Maui.Models;

[Table("SavedPredictions")]
public class PredictionDetailsEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public byte[] PlantImage { get; set; }
    public string TagName { get; set; }
    public int Probability { get; set; }
    public string Recommendation { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;
}
