namespace HungryMonster.Core.DTOs;

public record CreateMealRecordRequest(int Year, int NumberOfServings, int ClientId);

public record UpdateMealRecordRequest(int NumberOfServings);

public record MealRecordResponse(
    int Id,
    int Year,
    int NumberOfServings,
    int ClientId,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
