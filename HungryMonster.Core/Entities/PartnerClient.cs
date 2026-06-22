namespace HungryMonster.Core.Entities;

/// <summary>
/// Represents a strategic partner client operating in a specific industry.
/// Inherits from <see cref="Client"/> and receives a 25% catering discount.
/// </summary>
public class PartnerClient : Client
{
    /// <summary>The industry sector this partner operates in (e.g. "Technology", "Agriculture").</summary>
    public string Industry { get; private set; } = string.Empty;

    private PartnerClient() { }

    /// <summary>
    /// Creates a new partner client.
    /// </summary>
    /// <param name="name">Client display name.</param>
    /// <param name="industry">Industry sector the partner operates in.</param>
    public PartnerClient(string name, string industry) : base(name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(industry);
        Industry = industry;
    }

    /// <summary>Updates the industry sector for this partner.</summary>
    public void UpdateIndustry(string industry)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(industry);
        Industry = industry;
        SetUpdatedAt();
    }

    /// <summary>
    /// Partner clients receive a 25% discount, reflecting the strategic partnership.
    /// </summary>
    public override decimal CalculateDiscount() => 25.0m;
}
