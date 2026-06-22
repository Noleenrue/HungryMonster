namespace HungryMonster.Core.Entities;

/// <summary>
/// Represents a client who has a company registration number.
/// Inherits from <see cref="Client"/> and receives a 15% catering discount.
/// </summary>
public class ContractorClient : Client
{
    /// <summary>Official company registration number.</summary>
    public string CompanyNumber { get; private set; } = string.Empty;

    private ContractorClient() { }

    /// <summary>
    /// Creates a new contractor client.
    /// </summary>
    /// <param name="name">Client display name.</param>
    /// <param name="companyNumber">Official company registration number.</param>
    public ContractorClient(string name, string companyNumber) : base(name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(companyNumber);
        CompanyNumber = companyNumber;
    }

    /// <summary>Updates the company registration number.</summary>
    public void UpdateCompanyNumber(string companyNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(companyNumber);
        CompanyNumber = companyNumber;
        SetUpdatedAt();
    }

    /// <summary>
    /// Contractor clients receive a flat 15% discount.
    /// </summary>
    public override decimal CalculateDiscount() => 15.0m;
}
