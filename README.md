<p align="center"><img src="Docs/Capa-ListiFy_Api.png" alt="Capa do projeto ListiFy.Api" width="100%" /></p>

<h1 align="center">📚 ListiFy API</h1>

<p align="center"><em>API ASP.NET Core para gerenciar coleções pessoais.</em></p>

<p align="center">🇧🇷 Português | 🇺🇸 <a href="README.en.md">English</a></p>

<p align="center"><img src="https://img.shields.io/badge/licen%C3%A7a-MIT-006bed?style=flat-square" alt="MIT" /> <img src="https://img.shields.io/github/last-commit/Felipe-Amorim-Dev/ListiFy.Api?style=flat-square&color=006bed" alt="Last commit" /></p>

<p align="center"><a href="#sobre">Sobre</a> • <a href="#tecnologias">Tecnologias</a> • <a href="#como-rodar">Como rodar</a> • <a href="#licenca">Licença</a> • <a href="#autor">Autor</a></p>

---

<a id="sobre"></a>
## 📖 Sobre

Back-end para cadastro de usuários e itens de coleções, autenticação JWT, fotos e persistência relacional com Entity Framework Core e SQL Server.

## ✨ Funcionalidades

- Cadastro e autenticação de usuários
- Cadastro, consulta, edição e exclusão de itens
- Galeria de fotos dos itens
- Integração SMTP no cadastro

## 🛠️ Tecnologias

![C#](https://img.shields.io/badge/C%23-006bed?style=for-the-badge) ![.NET 8](https://img.shields.io/badge/.NET_8-006bed?style=for-the-badge) ![SQL Server](https://img.shields.io/badge/SQL_Server-006bed?style=for-the-badge) ![EF Core 8.0.6](https://img.shields.io/badge/EF_Core_8.0.6-006bed?style=for-the-badge) ![JWT](https://img.shields.io/badge/JWT-006bed?style=for-the-badge) ![Swagger](https://img.shields.io/badge/Swagger-006bed?style=for-the-badge)

## 📦 Pré-requisitos

SDK .NET 8 e SQL Server. Configure localmente conexão com o banco, JWT e SMTP.

<a id="como-rodar"></a>
## 🚀 Como rodar

```bash
git clone https://github.com/Felipe-Amorim-Dev/ListiFy.Api.git
cd ListiFy.Api
dotnet restore Listify.sln
dotnet build Listify.sln
cd Listify.Services
dotnet run --launch-profile http
```

Swagger: http://localhost:5274/swagger

## 🔗 Repositórios relacionados

- [ListiFy.App](https://github.com/Felipe-Amorim-Dev/ListiFy.App) — Interface Angular 16

<a id="licenca"></a>
## 📄 Licença

Distribuído sob a [licença MIT](LICENSE).

<a id="autor"></a>
## 👤 Autor

**Felipe Figueiredo Amorim** — Desenvolvedor Full Stack.

[![LinkedIn](https://img.shields.io/badge/LinkedIn-Felipe%20Amorim-0A66C2?style=for-the-badge&logo=linkedin)](https://www.linkedin.com/in/felipe-amorim-dev/) [![E-mail](https://img.shields.io/badge/E--mail-contato-006bed?style=for-the-badge)](mailto:felipe.f.amorim@outlook.com)

<p align="center"><strong>Obrigado por visitar! / Thanks for visiting!</strong></p>
