using HungryMonster.Core.Entities;

namespace HungryMonster.Core.Interfaces;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client?> GetClientByIdAsync(int id);
    Task<ContractorClient> AddContractorClientAsync(string name, string companyNumber);
    Task<PartnerClient> AddPartnerClientAsync(string name, string industry);
    Task UpdateClientNameAsync(int id, string name);
    Task DeleteClientAsync(int id);
    Task<decimal> GetClientDiscountAsync(int id);
}
