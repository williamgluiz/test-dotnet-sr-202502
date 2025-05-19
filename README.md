# Avaliação ASP.NET (API + EF + CQRS + DDD + Docker + Tests)

## Requisitos de negócio
![image](docs/modelagem.png)

Nossa equipe de frontend está desenvolvendo uma aplicação web que possibilita que recrutadores possam gerenciar a base de candidatos da empresa.
Sua missão é desenvolver um projeto do tipo WebAPI que possibilite que nossa equipe de frontend implemente as seguintes ações na UI:

- Consultar a lista de candidatos cadastrados.
- Cadastrar novos candidatos.
- Editar o cadastro de um candidato.
- Excluir um candidato.

Nosso sistema passa por auditorias frequentes e precisamos armazenar todas as ações realizadas pelos recrutadores.

Portanto ao cadastrar, editar ou excluir é necessário registrar uma timeline identificando a ação através de `IdTimelineType`,
também precisamos armazenar em formato string (JSON) nas colunas `OldData` e `NewData` os dados antigos e novos do candidato, respectivamente.

## Requisitos técnicos
- Desenvolva um projeto de WebAPI utilizando .NET Core 7.
- Implemente o padrão arquitetural CQRS (com o package MediatR ou similares).
- Implemente registro de timelines utilizando publicação/disparo de eventos.
- Implemente o padrão Repository & Unit Of Work Pattern.
- Desenvolva com base no modelo relacional apresentado.
- Desenvolva utilizando a abordagem de Code-First.
- Utilize o EF Core como ORM para consultas e persitência de dados.
- Utilize banco de dados Microsoft SQL Server através do Docker (modifique arquivos compose se achar necessário).
- Cria ou modifique as camadas/projetos que julgar necessário.
- Implemente os testes unitários e funcionais que julgar necessário.

## Como iniciar
- Faça um fork deste repositório.
- Ao finalizar o seu desenvolvimento, submeta um pull request para este repositório.

## Dicas
- Não é necessário implementar autenticação/autorização (JWT)
- Aplique os seus conhecimentos e boas práticas de OOP, DDD, SOLID e Clean Code.
- Não existe certo ou errado, o nosso objetivo é conhecer o seu estilo de programação.

-------------------------------------------------------------------------------------------------------------------

## ✅ Implementation Notes by William Gabriel Luiz

This section contains additional information and changes made in this fork for evaluation purposes.

# Applicant Tracking API

This is an API for managing candidates and tracking actions performed in an applicant tracking system (ATS). It was developed as part of a coding challenge and follows clean architecture principles.

## 🧩 Technologies Used

- ASP.NET Core 7
- MediatR
- Entity Framework Core (SQL Server)
- FluentValidation
- Swagger
- Logging with `ILogger<T>`.
- Docker (optional)
- User Secrets (for protecting sensitive data)

## 📌 Features

- Create, update, delete, and retrieve candidate data
- Logs all changes in a timeline for auditing
- Follows Clean Architecture and separation of concerns
- Command and Query separation using MediatR.
- Validation using FluentValidation
- Global error handling and logging with `ILogger<T>`
- Swagger UI for API documentation
- Configuration via `appsettings` and User Secrets
- Microsoft SQL Server with EF Core and Migrations.

### How to Run This Project

1. Ensure Docker is running if you're using containers for PostgreSQL.
2. Set up User Secrets for the connection string:


### 🚀 Getting Started
Prerequisites
.NET 7 SDK
Microsoft SQL Server
Docker
Setup
Clone the repository:

bash
Copy
Edit
git clone https://github.com/williamgluiz/test-dotnet-sr-202502.git
cd applicant-tracking-api

Apply EF Core migrations:

bash
Copy
Edit
dotnet ef database update --project ApplicantTracking.Infrastructure

Configure your connection string using User Secrets:
bash
Copy
Edit
cd ApplicantTracking.API
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5433;Database=ApplicantTrackingDb;Username=postgres;Password=yourpassword"

Running the API
bash
Copy
Edit
cd ApplicantTracking.API
dotnet run

Once the API is running, access the Swagger UI at:

bash
Copy
Edit
https://localhost:<port>/swagger/index.html

🔎 API Endpoints
All routes are prefixed with /api/candidates.

Method	Endpoint	Description

GET	/api/candidates	List all candidates
GET	/api/candidates/{id}	Get candidate by ID
POST	/api/candidates	Create a new candidate
PUT	/api/candidates/{id}	Update candidate
DELETE	/api/candidates/{id}	Delete candidate

📘 Logs
Logging is implemented using Serilog and writes to the console and optionally to files. The configuration can be found in Program.cs.

📂 Project Structure
plaintext
Copy
Edit
ApplicantTracking.API             --> Presentation Layer
ApplicantTracking.Application     --> Application Layer (CQRS, Handlers, Validators)
ApplicantTracking.Domain          --> Domain Models and Interfaces
ApplicantTracking.Infrastructure  --> Infrastructure Layer (EF Core, Repositories, DB Context)
🧪 Tests
Unit tests and integration tests can be added using xUnit or NUnit in a separate ApplicantTracking.Tests project.

📄 License
This project is licensed under the MIT License.
