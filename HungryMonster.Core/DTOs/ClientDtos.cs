namespace HungryMonster.Core.DTOs;

public record CreateContractorClientRequest(string Name, string CompanyNumber);

public record CreatePartnerClientRequest(string Name, string Industry);

public record UpdateClientNameRequest(string Name);

public record ClientResponse(
    int Id,
    string Name,
    string ClientType,
    string? CompanyNumber,
    string? Industry,
    decimal Discount,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
