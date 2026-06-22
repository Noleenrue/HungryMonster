namespace HungryMonster.Core.Entities;

/// <summary>
/// Represents a catering order record for a specific client in a given year.
/// </summary>
public class MealRecord : BaseEntity
{
    /// <summary>The calendar year this meal record belongs to.</summary>
    public int Year { get; private set; }

    /// <summary>Total number of meal servings ordered. Must be greater than zero.</summary>
    public int NumberOfServings { get; private set; }

    /// <summary>Foreign key referencing the associated <see cref="Client"/>.</summary>
    public int ClientId { get; private set; }

    /// <summary>Navigation property to the associated client.</summary>
    public Client? Client { get; private set; }

    private MealRecord() { }

    /// <summary>
    /// Creates a new meal record with validated inputs.
    /// </summary>
    /// <param name="year">Calendar year (2000 to current year + 1).</param>
    /// <param name="numberOfServings">Must be greater than zero.</param>
    /// <param name="clientId">Must be a valid positive client ID.</param>
    public MealRecord(int year, int numberOfServings, int clientId)
    {
        if (year < 2000 || year > DateTime.UtcNow.Year + 1)
            throw new ArgumentOutOfRangeException(nameof(year), "Year is out of valid range.");

        if (numberOfServings <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfServings), "Number of servings must be greater than zero.");

        if (clientId <= 0)
            throw new ArgumentOutOfRangeException(nameof(clientId), "ClientId must be a valid positive integer.");

        Year = year;
        NumberOfServings = numberOfServings;
        ClientId = clientId;
    }

    /// <summary>
    /// Updates the number of servings. Must be greater than zero.
    /// </summary>
    public void UpdateServings(int numberOfServings)
    {
        if (numberOfServings <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfServings), "Number of servings must be greater than zero.");

        NumberOfServings = numberOfServings;
        SetUpdatedAt();
    }
}
