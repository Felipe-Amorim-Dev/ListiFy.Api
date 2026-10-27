<p align="center"><img src="Docs/Capa-ListiFy_Api.png" alt="Project cover ListiFy.Api" width="100%" /></p>

<h1 align="center">📚 ListiFy API</h1>

<p align="center"><em>ASP.NET Core API for managing personal collections.</em></p>

<p align="center">🇧🇷 <a href="README.md">Português</a> | 🇺🇸 English</p>

<p align="center"><img src="https://img.shields.io/badge/license-MIT-006bed?style=flat-square" alt="MIT" /> <img src="https://img.shields.io/github/last-commit/Felipe-Amorim-Dev/ListiFy.Api?style=flat-square&color=006bed" alt="Last commit" /></p>

<p align="center"><a href="#sobre">About</a> • <a href="#tecnologias">Technologies</a> • <a href="#como-rodar">Running locally</a> • <a href="#licenca">License</a> • <a href="#autor">Author</a></p>

---

<a id="sobre"></a>
## 📖 About

Backend for user accounts and collection items, JWT authentication, images and relational persistence using Entity Framework Core and SQL Server.

## ✨ Features

- User registration and authentication
- Create, list, update and delete items
- Item photo gallery
- SMTP integration during registration

## 🛠️ Technologies

![C#](https://img.shields.io/badge/C%23-006bed?style=for-the-badge) ![.NET 8](https://img.shields.io/badge/.NET_8-006bed?style=for-the-badge) ![SQL Server](https://img.shields.io/badge/SQL_Server-006bed?style=for-the-badge) ![EF Core 8.0.6](https://img.shields.io/badge/EF_Core_8.0.6-006bed?style=for-the-badge) ![JWT](https://img.shields.io/badge/JWT-006bed?style=for-the-badge) ![Swagger](https://img.shields.io/badge/Swagger-006bed?style=for-the-badge)

## 📦 Prerequisites

.NET 8 SDK and SQL Server. Configure database connection, JWT and SMTP settings locally.

<a id="como-rodar"></a>
## 🚀 Running locally

```bash
git clone https://github.com/Felipe-Amorim-Dev/ListiFy.Api.git
cd ListiFy.Api
dotnet restore Listify.sln
dotnet build Listify.sln
cd Listify.Services
dotnet run --launch-profile http
```

Swagger: http://localhost:5274/swagger

## 🔗 Related repositories

- [ListiFy.App](https://github.com/Felipe-Amorim-Dev/ListiFy.App) — Interface Angular 16

## 🏗️ Architecture and responsibilities

- Listify.Services — HTTP controllers, request/response models, Swagger and dependency registration
- Listify.Domain — Usuario, Item and ItemFoto entities, contracts and business rules
- Listify.Infra.Data — DbContext, EF Core mappings, migrations and SQL Server repositories
- Listify.Security — JWT issuance with configurable expiration

### Execution flow

1. Angular → ASP.NET Core controllers → domain services → EF Core repositories → SQL Server
2. Item registration accepts form data and up to five images; ItemFoto links each image to its item.
3. Authentication looks up the user and issues a token through TokenSecurity.

### Local execution

```bash
dotnet restore Listify.sln
dotnet build Listify.sln
cd Listify.Services
dotnet run --launch-profile http
```

Project endpoint: http://localhost:5274/swagger

<a id="licenca"></a>
## 📄 License

Distributed under the [MIT license](LICENSE).

<a id="autor"></a>
## 👤 Author

**Felipe Figueiredo Amorim** — Full Stack Developer.

[![LinkedIn](https://img.shields.io/badge/LinkedIn-Felipe%20Amorim-0A66C2?style=for-the-badge&logo=linkedin)](https://www.linkedin.com/in/felipe-amorim-dev/) [![E-mail](https://img.shields.io/badge/E--mail-contato-006bed?style=for-the-badge)](mailto:felipe.f.amorim@outlook.com)

<p align="center"><strong>Obrigado por visitar! / Thanks for visiting!</strong></p>
