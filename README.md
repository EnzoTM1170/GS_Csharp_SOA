# MindCare - Inteligência Emocional Corporativa

## 👥 Membros do Grupo

- **Gabriel Borba** - RM553187
- **Enzo Teles** - RM553899
- **Pedro Henrique Mello Silva Alves** - RM554223

## 📋 Sobre o Projeto

O **MindCare** é uma plataforma integrada de monitoramento emocional e prevenção de estresse corporativo, desenvolvida em C# com WPF e API REST seguindo arquitetura SOA (Service-Oriented Architecture).

O sistema combina monitoramento fisiológico (via wearables) e análise emocional (via NLP) para identificar sinais precoces de desgaste mental, permitindo que empresas tomem ações preventivas antes que o burnout aconteça.

## 🎯 Objetivo

Prototipar uma solução criativa que represente o trabalho do futuro, aplicando:
- **POO (Programação Orientada a Objetos)**: Herança, polimorfismo e encapsulamento
- **Interface gráfica WPF**: Interface moderna e intuitiva
- **Propósito social**: Promover bem-estar e saúde mental no ambiente corporativo

## 🏗️ Arquitetura

O projeto está organizado em camadas seguindo os princípios de Clean Architecture e SOA:

```
MindCare/
├── MindCare.Domain/          # Camada de Domínio
│   ├── Entities/            # Entidades do domínio (Employee, HealthMetric, etc.)
│   ├── ValueObjects/        # Value Objects (ContactInfo, SentimentScore, etc.)
│   └── Enums/               # Enumeradores
│
├── MindCare.Application/     # Camada de Aplicação
│   ├── DTOs/                # Data Transfer Objects
│   ├── Interfaces/          # Contratos de serviços
│   ├── Services/            # Lógica de negócio
│   ├── Validators/          # Validações com FluentValidation
│   └── Mappings/            # AutoMapper profiles
│
├── MindCare.Infrastructure/  # Camada de Infraestrutura
│   ├── Data/                # Entity Framework DbContext
│   └── Migrations/          # Migrações do banco de dados
│
├── MindCare.API/            # API REST (ASP.NET Core)
│   └── Controllers/         # Endpoints REST
│
└── MindCare.WPF/            # Aplicação Desktop (WPF)
    ├── Views/               # Telas da aplicação
    ├── Models/              # Modelos para a UI
    ├── Services/             # Serviços de comunicação com API
    └── Converters/           # Conversores para binding
```

## 🚀 Tecnologias Utilizadas

### Backend (API)
- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 8.0**
- **SQL Server** (LocalDB)
- **FluentValidation** - Validação de entrada
- **AutoMapper** - Mapeamento de objetos
- **Swagger** - Documentação da API

### Frontend (WPF)
- **.NET 8.0**
- **WPF (Windows Presentation Foundation)**
- **HttpClient** - Comunicação com API REST
- **Data Binding** - MVVM pattern


## 🗄️ Modelo de Dados

### Entidades Principais

- **Employee**: Funcionário da empresa
- **HealthMetric**: Métricas de saúde (batimentos, sono, temperatura, etc.)
- **EmotionalAnalysis**: Análise emocional de textos/comunicações
- **StressAlert**: Alertas de estresse gerados pelo sistema

### Value Objects

- **ContactInfo**: Informações de contato
- **SentimentScore**: Score de sentimento (0-1) com confiança e emoção dominante
- **MetricTypeValueObject**: Encapsula lógica de validação de métricas

### Herança e Polimorfismo

- **BaseEntity**: Classe base abstrata com propriedades comuns (Id, CreatedAt, UpdatedAt, IsActive)
- Todas as entidades herdam de `BaseEntity` e implementam comportamentos polimórficos

## 🔌 Endpoints da API

### Employees
- `GET /api/Employees` - Listar todos os funcionários
- `GET /api/Employees/{id}` - Buscar funcionário por ID
- `POST /api/Employees` - Criar novo funcionário
- `PUT /api/Employees/{id}` - Atualizar funcionário
- `DELETE /api/Employees/{id}` - Deletar funcionário

### Health Metrics
- `GET /api/HealthMetrics` - Listar todas as métricas
- `GET /api/HealthMetrics/{id}` - Buscar métrica por ID
- `GET /api/HealthMetrics/employee/{employeeId}` - Métricas de um funcionário
- `POST /api/HealthMetrics` - Criar nova métrica
- `DELETE /api/HealthMetrics/{id}` - Deletar métrica

### Emotional Analyses
- `GET /api/EmotionalAnalyses` - Listar todas as análises
- `GET /api/EmotionalAnalyses/{id}` - Buscar análise por ID
- `GET /api/EmotionalAnalyses/employee/{employeeId}` - Análises de um funcionário
- `POST /api/EmotionalAnalyses` - Criar nova análise
- `DELETE /api/EmotionalAnalyses/{id}` - Deletar análise

### Stress Alerts
- `GET /api/StressAlerts` - Listar alertas ativos
- `GET /api/StressAlerts/{id}` - Buscar alerta por ID
- `GET /api/StressAlerts/employee/{employeeId}` - Alertas de um funcionário
- `POST /api/StressAlerts/{id}/acknowledge` - Reconhecer alerta
- `DELETE /api/StressAlerts/{id}` - Deletar alerta

### Dashboard
- `GET /api/Dashboard/summary` - Resumo do dashboard

## 🖥️ Interface WPF

### Telas Implementadas

1. **Dashboard** (`DashboardView`)
   - Cards com resumo (Total de funcionários, Ativos, Alto risco, Alertas)
   - Médias de estresse e qualidade do sono
   - Tabela de alertas recentes com ação de reconhecimento
   - Botão de atualização

2. **Monitoramento** (`MonitoringView`)
   - Lista de funcionários com filtro
   - Métricas de saúde por funcionário
   - Visualização de status (Normal/Anormal)
   - Atualização em tempo real

## 🛠️ Como Executar
### Clique aqui para saber como executar 👇👇
https://github.com/EnzoTM1170/GS_Csharp_SOA/blob/e9025d6328735750d444126e9865e49ecff158c4/COMO_EXECUTAR.md

## 📊 Funcionalidades

### Monitoramento Fisiológico
- Captura de dados de wearables (simulado)
- Métricas: Frequência cardíaca, Qualidade do sono, Temperatura corporal, Nível de estresse
- Detecção automática de valores anormais

### Análise Emocional
- Análise de sentimento de textos (simulado com NLP básico)
- Detecção de emoções dominantes
- Cálculo de risco baseado em score de sentimento

### Sistema de Alertas
- Geração automática de alertas baseados em métricas e análises
- Níveis de severidade (Low, Medium, High, Critical)
- Reconhecimento de alertas pelos gestores

### Dashboard Executivo
- Visão consolidada da saúde emocional da equipe
- Indicadores chave (KPIs)
- Alertas recentes com ações rápidas

## 🔒 Segurança e Validações

- **Validação de entrada**: FluentValidation em todos os DTOs
- **Prevenção de SQL Injection**: Entity Framework com parâmetros
- **Tratamento de exceções**: Try-catch em todos os controllers e serviços
- **Validação de tipos**: Enums validados
- **Sanitização**: Validação de formato de email, telefone, etc.

## 📝 Migrações do Banco de Dados

O projeto utiliza Entity Framework Migrations para versionamento do banco:

```bash
# Criar nova migração
dotnet ef migrations add NomeDaMigracao --project MindCare.Infrastructure --startup-project MindCare.API

# Aplicar migrações
dotnet ef database update --project MindCare.Infrastructure --startup-project MindCare.API
```

## 🧪 Testes de Carga

Para testes de carga, recomenda-se usar ferramentas como:
- **Apache JMeter**
- **Postman** (Collection Runner)
- **k6**
- **Visual Studio Load Test**

Exemplo de teste básico com curl:
```bash
# Teste de carga simples
for i in {1..100}; do
  curl -X GET http://localhost:5000/api/Dashboard/summary &
done
wait
```

## 🎓 Observações Acadêmicas

Este projeto atende aos requisitos de duas disciplinas:
1. **Challenge - O Futuro do Trabalho**: Aplicação WPF com POO
2. **Global Solution - SOA & WebServices**: API REST seguindo arquitetura SOA

### Pontos de Destaque

- ✅ **Herança**: `BaseEntity` como classe base para todas as entidades
- ✅ **Polimorfismo**: Métodos virtuais e override em entidades
- ✅ **Value Objects**: `ContactInfo`, `SentimentScore`, `MetricTypeValueObject`
- ✅ **Separação de Responsabilidades**: Camadas bem definidas
- ✅ **Validações Robustas**: FluentValidation em todos os endpoints
- ✅ **Tratamento de Erros**: Try-catch e logging em toda a aplicação
- ✅ **Migrations Versionadas**: Controle de versão do banco de dados
- ✅ **Interface Moderna**: WPF com design responsivo e intuitivo

## 🔮 Melhorias Futuras

- [ ] Integração real com APIs de wearables (Fitbit, Apple Health)
- [ ] Integração com APIs de NLP (Azure Text Analytics, AWS Comprehend)
- [ ] Autenticação e autorização (JWT)
- [ ] Notificações em tempo real (SignalR)
- [ ] Relatórios em PDF
- [ ] Gráficos e visualizações avançadas
- [ ] Testes unitários e de integração
- [ ] Deploy em cloud (Azure, AWS)

---

**Desenvolvido com ❤️ para promover bem-estar no ambiente corporativo**









