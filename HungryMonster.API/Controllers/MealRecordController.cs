using HungryMonster.Core.DTOs;
using HungryMonster.Core.Entities;
using HungryMonster.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HungryMonster.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealRecordController : ControllerBase
{
    private readonly IMealRecordService _mealRecordService;

    public MealRecordController(IMealRecordService mealRecordService)
    {
        _mealRecordService = mealRecordService;
    }

    // GET /api/mealrecord
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MealRecordResponse>>> GetAll()
    {
        var records = await _mealRecordService.GetAllMealRecordsAsync();
        return Ok(records.Select(MapToResponse));
    }

    // GET /api/mealrecord/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MealRecordResponse>> GetById(int id)
    {
        var record = await _mealRecordService.GetMealRecordByIdAsync(id);
        if (record is null)
            return NotFound($"MealRecord with id {id} was not found.");

        return Ok(MapToResponse(record));
    }

    // POST /api/mealrecord
    [HttpPost]
    public async Task<ActionResult<MealRecordResponse>> Create([FromBody] CreateMealRecordRequest request)
    {
        var record = await _mealRecordService.AddMealRecordAsync(
            request.Year, request.NumberOfServings, request.ClientId);

        return CreatedAtAction(nameof(GetById), new { id = record.Id }, MapToResponse(record));
    }

    // PUT /api/mealrecord/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMealRecordRequest request)
    {
        var existing = await _mealRecordService.GetMealRecordByIdAsync(id);
        if (existing is null)
            return NotFound($"MealRecord with id {id} was not found.");

        await _mealRecordService.UpdateServingsAsync(id, request.NumberOfServings);
        return NoContent();
    }

    // DELETE /api/mealrecord/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _mealRecordService.GetMealRecordByIdAsync(id);
        if (existing is null)
            return NotFound($"MealRecord with id {id} was not found.");

        await _mealRecordService.DeleteMealRecordAsync(id);
        return NoContent();
    }

    // GET /api/mealrecord/peak-year
    [HttpGet("peak-year")]
    public async Task<ActionResult<PeakYearResult>> GetPeakYear()
    {
        var result = await _mealRecordService.GetPeakYearAsync();
        if (result is null)
            return NotFound("No meal records found.");

        return Ok(result);
    }

    private static MealRecordResponse MapToResponse(MealRecord r) =>
        new(r.Id, r.Year, r.NumberOfServings, r.ClientId, r.CreatedAt, r.UpdatedAt);
}
