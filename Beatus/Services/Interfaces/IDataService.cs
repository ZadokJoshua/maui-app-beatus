using Beatus.Models;

namespace Beatus.Services.Interfaces;

public interface IDataService
{
    Task<int> DeletePrediction(PredictionDetailsEntity entity);
    Task<List<PredictionDetailsEntity>> GetAllSavedPredictionsAsync();
    Task<PredictionDetailsEntity> GetSavedPrediction(int id);
    Task<int> SavePrediction(PredictionDetailsEntity entity);
}
