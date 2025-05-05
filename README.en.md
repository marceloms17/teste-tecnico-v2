📄 Available in: [English](README.en.md) | [Português](README.md)

# 🚧 Technical Challenge – Thunders Tecnologia

This project was developed as part of the technical challenge for a Senior Software Engineer position at Thunders Tecnologia. The challenge involved creating a scalable API capable of ingesting and processing real-time toll usage data, focusing on performance, clean architecture, and engineering best practices.

---

## 🎯 Challenge Goal

Build a robust and scalable system to handle millions of toll usage records and generate reports by city, toll station, hour, and vehicle type using asynchronous messaging and a resilient architecture.

---

## 🚀 Technologies Used

- ASP.NET Core (.NET 8)
- C#
- CQRS with MediatR
- Entity Framework Core
- RabbitMQ + Rebus (messaging)
- SQL Server
- OpenTelemetry
- Docker + Aspire
- Unit Testing

---

## 🧱 Architecture

- Layered architecture (`Application`, `Controllers`, `Handlers`, `DTOs`)
- CQRS pattern (Commands, Queries, Handlers)
- Rebus for asynchronous messaging
- Repository pattern and dependency injection
- Fully testable and observable handlers

---

## 📂 Project Structure

```
teste-tecnico-v2/
├── Thunders.TechTest.ApiService/
│   ├── Application/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── Handlers/
│   │   ├── DTOs/
│   │   └── Consumers/
│   ├── Controllers/
│   ├── Program.cs
│   └── appsettings.json
├── Thunders.TechTest.Abstractions/
│   └── IMessageSender.cs
├── Thunders.TechTest.sln
```

---

## 📌 Implemented Features

- API for toll usage data ingestion
- Data persistence using EF Core
- Trigger via API endpoint for report generation:
  - Total revenue by hour per city
  - Top earning stations by month
  - Vehicle types per toll station
- Asynchronous message processing with Rebus
- Unit tests for application handlers

---

## ▶️ How to Run

1. Clone the repository:
```bash
git clone https://github.com/marceloms17/teste-tecnico-v2.git
```

2. Start the AppHost project using .NET Aspire tools

3. Access Swagger at:
```
http://localhost:5000/swagger
```

---

## 👤 Author

Marcelo Morais dos Santos  
📧 marceloms17@gmail.com  
🔗 [LinkedIn](https://www.linkedin.com/in/marcelo-morais-61584146)
