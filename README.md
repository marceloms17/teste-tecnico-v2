📄 Disponível em: [Português](README.md) | [English](README.en.md)

# 🚧 Teste Técnico – Thunders Tecnologia

Este projeto foi desenvolvido como parte do desafio técnico para a vaga de Engenheiro de Software Sênior na Thunders Tecnologia. O desafio propõe a criação de uma API escalável para ingestão e processamento de dados de praças de pedágio, com foco em performance, organização arquitetural e boas práticas de engenharia.

---

## 🎯 Objetivo do Desafio

Desenvolver um sistema capaz de receber milhões de registros de utilização de pedágios em tempo real e gerar relatórios de faturamento por hora, por cidade, por praça e por tipo de veículo, com apoio de mensageria assíncrona e arquitetura resiliente.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core (.NET 8)
- C#
- CQRS com MediatR
- Entity Framework Core
- RabbitMQ + Rebus (mensageria)
- SQL Server
- OpenTelemetry (monitoramento)
- Docker + Aspire
- Testes Unitários

---

## 🧱 Arquitetura

- Separação por camadas (`Application`, `Controllers`, `Handlers`, `DTOs`)
- Uso de CQRS (Commands, Queries e Handlers)
- Rebus para mensageria assíncrona com RabbitMQ
- Repository Pattern e injeção de dependência
- Handlers desacoplados com testes e rastreamento

---

## 📂 Estrutura

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

## 📌 Funcionalidades Implementadas

- API para ingestão de dados de utilização de pedágio
- Armazenamento em banco de dados via EF Core
- Gatilho via endpoint para geração de relatórios:
  - Receita total por hora por cidade
  - Praças que mais faturaram por mês
  - Tipos de veículos que passaram por uma praça
- Mensageria com consumidores Rebus
- Testes unitários para handlers

---

## ▶️ Como Executar

1. Clonar o repositório:
```bash
git clone https://github.com/marceloms17/teste-tecnico-v2.git
```

2. Build com .NET Aspire:
```bash
Set o AppHost como startup project e inicie a aplicação
```

3. Acesse:
```
http://localhost:5000/swagger
```

---

## 👤 Autor

Marcelo Morais dos Santos  
📧 marceloms17@gmail.com  
🔗 [LinkedIn](https://www.linkedin.com/in/marcelo-morais-61584146)
