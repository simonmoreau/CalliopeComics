# CalliopeComics AGENTS.md

## Architecture
- Uses Clean Architecture with separate projects:
  - `src/Domain`: Domain entities and logic
  - `src/Application`: Application services and business logic
  - `src/Infrastructure`: Data access and external services
  - `src/WebApp`: ASP.NET Core REST API entry point

## Development Setup
- **Local Development**: Run from `src/WebApp` directory using `dotnet run`
  - Profiles from `launchSettings.json`:
    - HTTP: `http://localhost:5095` (swagger UI launches)
    - HTTPS: `https://localhost:7296`
- **Docker Development**: `docker-compose -f docker/docker-compose.yml up --build`
  - App: `http://localhost:5678`
  - Postgres: `localhost:5432`
- **Database**: PostgreSQL required; connections in `appsettings.json`
  - Main DB: `CalliopeComics`
  - Log DB: `CalliopeComics_log`
- **Storage Paths** (hardcoded in config):
  - Raw files: `C:\aa\CalliopeComics\`
  - Processed: `C:\aa\CalliopeComics\processed\`
  - Docker: `/data/CalliopeComics` (mapped to host)
- **API Keys**: Gemini API key required in `appsettings.json`

## Commands
- **Build**: `dotnet build`
- **Run Tests**: `dotnet test` (from solution root or test project)
- **Migrations**: App auto-runs `dotnet WebApp.dll --migrate` on startup in Docker

## Coding Standards
- See `.github/copilot-instructions.md` for C# conventions, formatting, and project-specific guidelines</content>
<parameter name="filePath">/mnt/c/Users/smoreau/Github/Divers/CalliopeComics/AGENTS.md