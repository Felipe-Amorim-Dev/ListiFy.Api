# 🚀 Listify API

API REST robusta desenvolvida em .NET, com foco em arquitetura limpa, segurança e escalabilidade, voltada para gerenciamento de usuários, itens e imagens.

![GitHub repo size](https://img.shields.io/github/repo-size/Felipe-Amorim-Dev/listify?style=for-the-badge)
![GitHub stars](https://img.shields.io/github/stars/Felipe-Amorim-Dev/listify?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/Felipe-Amorim-Dev/listify?style=for-the-badge)

## 📌 Visão Geral

O Listify é uma API backend projetada para fornecer uma base sólida para aplicações modernas, seguindo boas práticas de engenharia de software.

**O projeto foi estruturado com foco em:**

🔹 Organização em camadas (DDD simplificado)

🔹 Baixo acoplamento e alta coesão

🔹 Segurança com autenticação JWT

🔹 Facilidade de manutenção e evolução

🔹 Separação clara de responsabilidades

## 🧱 Arquitetura

A solução segue uma arquitetura em camadas bem definida:

```
Listify
│
├── Listify.Domain        → Entidades, regras de negócio, interfaces
├── Listify.Infra.Data    → Persistência (EF Core, repositórios)
├── Listify.Security      → Autenticação e geração de tokens JWT
├── Listify.Services      → API (Controllers, DI, configuração)
```

## 🔍 Padrões utilizados

Repository Pattern

Dependency Injection

Separation of Concerns

Configuração centralizada via appsettings

## ⚙️ Stack Tecnológica

![.NET](https://img.shields.io/badge/.NET-API-blue?style=for-the-badge&logo=dotnet)
![Security](https://img.shields.io/badge/security-JWT-yellow?style=for-the-badge)
![Database](https://img.shields.io/badge/database-SQL%20Server-blue?style=for-the-badge&logo=microsoftsqlserver)

## 🔐 Segurança

A API implementa autenticação baseada em JWT, garantindo proteção dos endpoints.

## ✔️ Recursos de segurança

```
Autenticação via token
Proteção de rotas com [Authorize]
Segregação de configuração sensível
Suporte a variáveis de ambiente
```

## ⚠️ Importante:
Nenhuma credencial sensível está versionada neste repositório.

## 🚀 Como executar o projeto

### 📥 1. Clonar o repositório

git clone https://github.com/seu-usuario/listify.git
cd listify

### ⚙️ 2. Configuração inicial

Crie o arquivo de configuração baseado no exemplo:

cp Listify.Services/appsettings.Example.json Listify.Services/appsettings.json

### 🔧 3. Ajustar configurações

Edite o arquivo:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "SUA_CONNECTION_STRING"
  },
  "Jwt": {
    "SecretKey": "SUA_CHAVE_SUPER_SECRETA",
    "ExpirationInMinutes": 60
  },
  "Smtp": {
    "Server": "smtp.seudominio.com",
    "Port": "587",
    "Username": "seu-email",
    "Password": "sua-senha"
  }
}
```

### 🗄️ 4. Aplicar migrations

```
dotnet ef database update
```

### ▶️ 5. Executar a aplicação

```
dotnet run --project Listify.Services
```

### 🌐 6. Acessar documentação (Swagger)

```
https://localhost:{porta}/swagger
```

### 🔑 Fluxo de autenticação

Usuário realiza login

API retorna um token JWT

Cliente envia o token nas requisições:

Authorization: Bearer {seu_token}

### 📬 Envio de e-mails

O sistema possui integração com SMTP para envio de e-mails.

**Configuração necessária:**

```
"Smtp": {
  "Server": "",
  "Port": "",
  "Username": "",
  "Password": ""
}
```

### 👨‍💻 Autor

Felipe Amorim
Fullstack Developer

🔗 GitHub: https://github.com/Felipe-Amorim-Dev

🔗 LinkedIn: https://www.linkedin.com/in/felipe-amorim-dev/