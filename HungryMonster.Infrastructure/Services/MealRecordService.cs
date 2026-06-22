using HungryMonster.Core.DTOs;
using HungryMonster.Core.Entities;
using HungryMonster.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using HungryMonster.Infrastructure.Data;

namespace HungryMonster.Infrastructure.Services;

public class MealRecordService : IMealRecordService
{
    private readonly IRepository<MealRecord> _mealRecordRepo;
    private readonly HungryMonsterDbContext _context;

    public MealRecordService(
        IRepository<MealRecord> mealRecordRepo,
        HungryMonsterDbContext context)
    {
        _mealRecordRepo = mealRecordRepo;
        _context = context;
    }

    public async Task<IEnumerable<MealRecord>> GetAllMealRecordsAsync() =>
        await _mealRecordRepo.GetAllAsync();

    public async Task<MealRecord?> GetMealRecordByIdAsync(int id) =>
        await _mealRecordRepo.GetByIdAsync(id);

    public async Task<IEnumerable<MealRecord>> GetMealRecordsByClientAsync(int clientId) =>
        await _context.MealRecords
            .AsNoTracking()
            .Where(m => m.ClientId == clientId)
            .ToListAsync();

    public async Task<MealRecord> AddMealRecordAsync(int year, int numberOfServings, int clientId)
    {
        var record = new MealRecord(year, numberOfServings, clientId);
        return await _mealRecordRepo.AddAsync(record);
    }

    public async Task UpdateServingsAsync(int id, int numberOfServings)
    {
        var record = await _mealRecordRepo.GetByIdAsync(id);
        if (record is null)
            throw new KeyNotFoundException($"MealRecord with id {id} was not found.");

        record.UpdateServings(numberOfServings);
        await _mealRecordRepo.UpdateAsync(record);
    }

    public async Task DeleteMealRecordAsync(int id) =>
        await _mealRecordRepo.DeleteAsync(id);

    public async Task<PeakYearResult?> GetPeakYearAsync()
    {
        // Group active records by year, count distinct companies per year,
        // then return the year with the highest count.
        var result = await _context.MealRecords
            .AsNoTracking()
            .Where(m => m.NumberOfServings > 0)
            .GroupBy(m => m.Year)
            .Select(g => new PeakYearResult(
                g.Key,
                g.Select(m => m.ClientId).Distinct().Count()))
            .OrderByDescending(r => r.ActiveCompaniesCount)
            .FirstOrDefaultAsync();

        return result;
    }
}
