# HungryMonster — Catering Management System

A full-stack .NET 8 catering management application demonstrating core OOP principles, clean architecture, and modern ASP.NET Core patterns.

---

## Solution Structure

```
HungryMonster.sln
├── HungryMonster.API           ASP.NET Core Web API (REST endpoints + Swagger)
├── HungryMonster.Core          Domain layer (Entities, Interfaces, DTOs)
├── HungryMonster.Infrastructure EF Core, Repositories, Services
└── HungryMonster.UI            WinForms desktop client
```

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or SQL Server LocalDB (`(localdb)\mssqllocaldb`)
- Visual Studio 2022 or VS Code

---

## Getting Started

### 1. Apply Database Migrations

```bash
dotnet ef database update \
  --project HungryMonster.Infrastructure \
  --startup-project HungryMonster.API
```

This creates the `HungryMonsterDb` database and seeds 5 clients and 10 meal records.

### 2. Run the API

```bash
cd HungryMonster.API
dotnet run
```

API runs at: `https://localhost:7237`  
Swagger UI: `https://localhost:7237/swagger`

### 3. Run the WinForms UI

```bash
cd HungryMonster.UI
dotnet run
```

> Ensure the API is running before launching the UI.

---

## API Endpoints

### Clients

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/client` | Get all clients |
| GET | `/api/client/{id}` | Get client by ID |
| POST | `/api/client` | Create a new client |
| PUT | `/api/client/{id}` | Update client name |
| DELETE | `/api/client/{id}` | Delete client |

**POST body example (contractor):**
```json
{
  "clientType": "contractor",
  "name": "Acme Ltd",
  "companyNumber": "CRN999"
}
```

**POST body example (partner):**
```json
{
  "clientType": "partner",
  "name": "Globex Corp",
  "industry": "Technology"
}
```

### Meal Records

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/mealrecord` | Get all meal records |
| GET | `/api/mealrecord/{id}` | Get meal record by ID |
| POST | `/api/mealrecord` | Create a meal record |
| PUT | `/api/mealrecord/{id}` | Update number of servings |
| DELETE | `/api/mealrecord/{id}` | Delete meal record |
| GET | `/api/mealrecord/peak-year` | Year with most active companies |

---

## OOP Concepts Demonstrated

| Concept | Where |
|---------|-------|
| **Encapsulation** | All entity properties use `private set`; state changes only via domain methods |
| **Inheritance** | `BaseEntity` → `Client` → `ContractorClient` / `PartnerClient` |
| **Polymorphism** | `abstract CalculateDiscount()` overridden per client type (15% / 25%) |
| **Dependency Injection** | Constructor injection throughout; registered in `InfrastructureServiceExtensions` |
| **Repository Pattern** | Generic `IRepository<T>` / `Repository<T>` backed by EF Core |

---

## Architecture

```
WinForms UI  ──(HttpClient)──▶  ASP.NET Core API
                                      │
                               IClientService
                               IMealRecordService
                                      │
                              IRepository<T>
                              Repository<T>
                                      │
                          HungryMonsterDbContext
                                      │
                              SQL Server (LocalDB)
```

---

## Database Schema

```
Clients (TPH)
├── Id (PK)
├── Name
├── ClientType   "Contractor" | "Partner"
├── CompanyNumber  (nullable — Contractor only)
├── Industry       (nullable — Partner only)
├── CreatedAt
└── UpdatedAt

MealRecords
├── Id (PK)
├── Year
├── NumberOfServings
├── ClientId (FK → Clients.Id)
├── CreatedAt
└── UpdatedAt
```

---

## Seed Data

3 Contractor clients (BuildRight Ltd, ConstructCo, SteelWorks Inc)  
2 Partner clients (GreenLeaf Partners, TechBridge Corp)  
10 Meal records across 2022–2024 — peak year is **2023** (5 active companies)

---

## Connection String

Configured in `HungryMonster.API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HungryMonsterDb;Trusted_Connection=True"
}
```

