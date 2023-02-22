using Beatus.Maui.Models;
using Beatus.Maui.Services.Interfaces;
using SQLite;

namespace Beatus.Maui.Services;

public class DataService : IDataService
{
    private readonly SQLiteAsyncConnection _database;

    public DataService()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PlantDiseasesDatabase.db");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<PredictionDetailsEntity>();
    }

    public async Task<List<PredictionDetailsEntity>> GetAllSavedPredictionsAsync()
    {
        return await _database.Table<PredictionDetailsEntity>().ToListAsync();
    }

    public async Task<PredictionDetailsEntity> GetSavedPRediction(int id)
    {
        return await _database.Table<PredictionDetailsEntity>().FirstOrDefaultAsync(pd => pd.Id == id);
    }

    public async Task<int> SavePrediction(PredictionDetailsEntity entity)
    {
        return await _database.InsertAsync(entity);
    }

    public async Task<int> DeletePrediction(PredictionDetailsEntity entity)
    {
        return await _database.InsertAsync(entity);
    }
}
