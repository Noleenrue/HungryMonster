using HungryMonster.Core.Entities;
using HungryMonster.Core.Interfaces;

namespace HungryMonster.Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly IRepository<ContractorClient> _contractorRepo;
    private readonly IRepository<PartnerClient> _partnerRepo;

    public ClientService(
        IRepository<ContractorClient> contractorRepo,
        IRepository<PartnerClient> partnerRepo)
    {
        _contractorRepo = contractorRepo;
        _partnerRepo = partnerRepo;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        var contractors = await _contractorRepo.GetAllAsync();
        var partners = await _partnerRepo.GetAllAsync();
        return contractors.Cast<Client>().Concat(partners.Cast<Client>());
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        Client? client = await _contractorRepo.GetByIdAsync(id);
        return client ?? await _partnerRepo.GetByIdAsync(id);
    }

    public async Task<ContractorClient> AddContractorClientAsync(string name, string companyNumber)
    {
        var client = new ContractorClient(name, companyNumber);
        return await _contractorRepo.AddAsync(client);
    }

    public async Task<PartnerClient> AddPartnerClientAsync(string name, string industry)
    {
        var client = new PartnerClient(name, industry);
        return await _partnerRepo.AddAsync(client);
    }

    public async Task UpdateClientNameAsync(int id, string name)
    {
        var contractor = await _contractorRepo.GetByIdAsync(id);
        if (contractor is not null)
        {
            contractor.UpdateName(name);
            await _contractorRepo.UpdateAsync(contractor);
            return;
        }

        var partner = await _partnerRepo.GetByIdAsync(id);
        if (partner is not null)
        {
            partner.UpdateName(name);
            await _partnerRepo.UpdateAsync(partner);
        }
    }

    public async Task DeleteClientAsync(int id)
    {
        var contractor = await _contractorRepo.GetByIdAsync(id);
        if (contractor is not null)
        {
            await _contractorRepo.DeleteAsync(id);
            return;
        }

        await _partnerRepo.DeleteAsync(id);
    }

    public async Task<decimal> GetClientDiscountAsync(int id)
    {
        var client = await GetClientByIdAsync(id);
        if (client is null)
            throw new KeyNotFoundException($"Client with id {id} was not found.");

        return client.CalculateDiscount();
    }
}
