using HungryMonster.Core.Entities;
using HungryMonster.Core.Interfaces;
using HungryMonster.Infrastructure.Repositories;
using HungryMonster.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HungryMonster.Infrastructure.Data;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<HungryMonsterDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Repositories
        services.AddScoped<IRepository<ContractorClient>, Repository<ContractorClient>>();
        services.AddScoped<IRepository<PartnerClient>, Repository<PartnerClient>>();
        services.AddScoped<IRepository<MealRecord>, Repository<MealRecord>>();

        // Services
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IMealRecordService, MealRecordService>();

        return services;
    }
}
