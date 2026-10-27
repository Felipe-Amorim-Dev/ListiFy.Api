# ListiFy API — .NET backend

[![Compilação .NET](https://github.com/Felipe-Amorim-Dev/ListiFy.Api/actions/workflows/ci.yml/badge.svg)](https://github.com/Felipe-Amorim-Dev/ListiFy.Api/actions/workflows/ci.yml)

**Segurança:** os artefatos da pasta de publicação foram removidos da versão atual do repositório. Configurações antigas podem permanecer no histórico e exigem revisão e, se válidas, troca de credenciais no provedor.


**Portfolio project / Projeto de portfólio.** ASP.NET Core backend supporting the [ListiFy Angular app](https://github.com/Felipe-Amorim-Dev/ListiFy.App). Intended to demonstrate API development, layered separation and relational persistence; not certified production-ready.

## Project layout
- `Listify.Domain` — domain models and abstractions
- `Listify.Infra.Data` — persistence
- `Listify.Security` — security concerns
- `Listify.Services` — ASP.NET Core API and composition
- `Listify.sln` — solution

The repository describes JWT authentication and SQL Server integration; validate their actual behavior and all configuration before deploying.

## Local setup
```bash
git clone https://github.com/Felipe-Amorim-Dev/ListiFy.Api.git
cd ListiFy.Api
dotnet restore Listify.sln
dotnet build Listify.sln
```
Configure SQL Server connectivity and relevant JWT/SMTP settings through local configuration or a secret manager. Review existing appsettings and migration setup before trying to launch the API. If using Entity Framework migrations, ensure your selected startup project and connection are correct.

## End-to-end application
Run the [Angular frontend](https://github.com/Felipe-Amorim-Dev/ListiFy.App) separately and configure its API base URL. Validate authentication, collection CRUD and image handling against a local dataset.

## Current verification status
The existence of project layers or documented features does not establish passing tests, coverage, load performance or security hardening. Build, integration testing and secret review remain necessary before promoting the project as production-ready.

**PT-BR:** API em .NET do projeto ListiFy; integra-se ao frontend Angular. Comandos de build documentados, mas a validação de ponta a ponta e de segurança continua necessária.
