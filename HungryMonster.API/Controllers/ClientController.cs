using HungryMonster.Core.DTOs;
using HungryMonster.Core.Entities;
using HungryMonster.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HungryMonster.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    // GET /api/client
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientResponse>>> GetAll()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients.Select(MapToResponse));
    }

    // GET /api/client/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientResponse>> GetById(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client is null)
            return NotFound($"Client with id {id} was not found.");

        return Ok(MapToResponse(client));
    }

    // POST /api/client  — body determines type via ClientType ("contractor" | "partner")
    [HttpPost]
    public async Task<ActionResult<ClientResponse>> Create([FromBody] CreateClientRequest request)
    {
        if (request.ClientType.Equals("contractor", StringComparison.OrdinalIgnoreCase))
        {
            if (string.IsNullOrWhiteSpace(request.CompanyNumber))
                return BadRequest("CompanyNumber is required for a contractor client.");

            var contractor = await _clientService.AddContractorClientAsync(request.Name, request.CompanyNumber);
            return CreatedAtAction(nameof(GetById), new { id = contractor.Id }, MapToResponse(contractor));
        }

        if (request.ClientType.Equals("partner", StringComparison.OrdinalIgnoreCase))
        {
            if (string.IsNullOrWhiteSpace(request.Industry))
                return BadRequest("Industry is required for a partner client.");

            var partner = await _clientService.AddPartnerClientAsync(request.Name, request.Industry);
            return CreatedAtAction(nameof(GetById), new { id = partner.Id }, MapToResponse(partner));
        }

        return BadRequest("ClientType must be 'contractor' or 'partner'.");
    }

    // PUT /api/client/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClientNameRequest request)
    {
        var existing = await _clientService.GetClientByIdAsync(id);
        if (existing is null)
            return NotFound($"Client with id {id} was not found.");

        await _clientService.UpdateClientNameAsync(id, request.Name);
        return NoContent();
    }

    // DELETE /api/client/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _clientService.GetClientByIdAsync(id);
        if (existing is null)
            return NotFound($"Client with id {id} was not found.");

        await _clientService.DeleteClientAsync(id);
        return NoContent();
    }

    private static ClientResponse MapToResponse(Client client) => client switch
    {
        ContractorClient c => new ClientResponse(c.Id, c.Name, "Contractor", c.CompanyNumber, null, c.CalculateDiscount(), c.CreatedAt, c.UpdatedAt),
        PartnerClient p    => new ClientResponse(p.Id, p.Name, "Partner",    null, p.Industry,   p.CalculateDiscount(), p.CreatedAt, p.UpdatedAt),
        _                  => new ClientResponse(client.Id, client.Name, "Unknown", null, null, 0, client.CreatedAt, client.UpdatedAt)
    };
}

// Unified create request — ClientType drives which subtype gets created
public record CreateClientRequest(string ClientType, string Name, string? CompanyNumber, string? Industry);
