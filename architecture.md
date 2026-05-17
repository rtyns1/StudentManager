# Architecture

## Solution Structure
StudentManager.sln
├── StudentManager.API          # ASP.NET Core Web API
│   ├── Controllers/            # REST endpoints
│   ├── Models/                 # EF Core entities
│   ├── Data/                   # DbContext, migrations
│   ├── Services/               # Email, Hangfire jobs
│   └── Program.cs
├── StudentManager.BlazorClient # Blazor WASM frontend
│   ├── Pages/                  # UI components
│   ├── Services/               # HTTP calls to API
│   └── Program.cs
└── docs/                       

## Data Flow
Blazor UI → API Controllers → Services → Database
Scheduled emails → Hangfire → EmailService → SMTP

## Deployment
- API and Blazor can be hosted together (API serves static files) or separately.
- For MVP, run both locally.