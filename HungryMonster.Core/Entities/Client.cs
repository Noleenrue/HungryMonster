namespace HungryMonster.Core.Entities;

public abstract class Client : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    private readonly List<MealRecord> _mealRecords = [];
    public IReadOnlyCollection<MealRecord> MealRecords => _mealRecords.AsReadOnly();

    protected Client() { }

    protected Client(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    public void UpdateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
        SetUpdatedAt();
    }

    /// <summary>
    /// Returns the discount percentage (0–100) applicable to this client type.
    /// </summary>
    public abstract decimal CalculateDiscount();
}
