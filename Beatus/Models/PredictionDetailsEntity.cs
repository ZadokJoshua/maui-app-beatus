using SQLite;

namespace Beatus.Models;

[Table("SavedPredictions")]
public class PredictionDetailsEntity : PredictionDetails
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;

    public PredictionDetailsEntity()
    {
    }
}
