using HungryMonster.Core.DTOs;
using HungryMonster.Core.Entities;

namespace HungryMonster.Core.Interfaces;

public interface IMealRecordService
{
    Task<IEnumerable<MealRecord>> GetAllMealRecordsAsync();
    Task<MealRecord?> GetMealRecordByIdAsync(int id);
    Task<IEnumerable<MealRecord>> GetMealRecordsByClientAsync(int clientId);
    Task<MealRecord> AddMealRecordAsync(int year, int numberOfServings, int clientId);
    Task UpdateServingsAsync(int id, int numberOfServings);
    Task DeleteMealRecordAsync(int id);

    /// <summary>
    /// Returns the year with the highest number of distinct companies
    /// that had at least one MealRecord where NumberOfServings > 0.
    /// </summary>
    Task<PeakYearResult?> GetPeakYearAsync();
}
