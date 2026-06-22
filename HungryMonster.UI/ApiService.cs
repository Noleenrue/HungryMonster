using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HungryMonster.UI;

public record ClientResponse(
    int Id,
    string Name,
    string ClientType,
    string? CompanyNumber,
    string? Industry,
    decimal Discount,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public record MealRecordResponse(
    int Id,
    int Year,
    int NumberOfServings,
    int ClientId,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public record CreateMealRecordRequest(int Year, int NumberOfServings, int ClientId);
public record UpdateMealRecordRequest(int NumberOfServings);
public record PeakYearResult(int Year, int ActiveCompaniesCount);

public class ApiService
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public ApiService(HttpClient http) => _http = http;

    public async Task<List<ClientResponse>> GetClientsAsync()
    {
        var result = await _http.GetFromJsonAsync<List<ClientResponse>>("api/client", _jsonOptions);
        return result ?? [];
    }

    public async Task<List<MealRecordResponse>> GetMealRecordsAsync()
    {
        var result = await _http.GetFromJsonAsync<List<MealRecordResponse>>("api/mealrecord", _jsonOptions);
        return result ?? [];
    }

    public async Task<MealRecordResponse?> AddMealRecordAsync(int year, int numberOfServings, int clientId)
    {
        var response = await _http.PostAsJsonAsync("api/mealrecord",
            new CreateMealRecordRequest(year, numberOfServings, clientId), _jsonOptions);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MealRecordResponse>(_jsonOptions);
    }

    public async Task UpdateMealRecordAsync(int id, int numberOfServings)
    {
        var response = await _http.PutAsJsonAsync($"api/mealrecord/{id}",
            new UpdateMealRecordRequest(numberOfServings), _jsonOptions);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteMealRecordAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/mealrecord/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<PeakYearResult?> GetPeakYearAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<PeakYearResult>("api/mealrecord/peak-year", _jsonOptions);
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}
