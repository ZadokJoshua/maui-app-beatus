using Beatus.Maui.Models;

namespace Beatus.Maui.Services.Interfaces;

public interface IDataService
{
    Task<int> DeletePrediction(PredictionDetailsEntity entity);
    Task<List<PredictionDetailsEntity>> GetAllSavedPredictionsAsync();
    Task<PredictionDetailsEntity> GetSavedPRediction(int id);
    Task<int> SavePrediction(PredictionDetailsEntity entity);
}
