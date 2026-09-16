# Delta

Sistema de gerenciamento de eventos.

## 🚀 Tecnologias

- ASP.NET Core 6.0
- Entity Framework Core
- SQL Server
- Identity para autenticação
- xUnit para testes
- AdminLTE para interface

## 📋 Pré-requisitos

- .NET 6.0 SDK
- SQL Server (LocalDB ou Express)
- Visual Studio 2022 ou VS Code

## 🔧 Instalação

1. Clone o repositório:
```bash
git clone https://github.com/mahalverissimo-rgb/delta.git
```

2. Restaure os pacotes:
```bash
dotnet restore
```

3. Atualize o banco de dados:
```bash
dotnet ef database update
```

4. Execute o projeto:
```bash
dotnet run
```

## 🗄️ Estrutura do Projeto

```
VillaBisutti.Delta/
├── VillaBisutti.Delta.New/           # Projeto principal
│   ├── Controllers/                   # Controladores MVC
│   ├── Models/                        # Classes de modelo
│   ├── Views/                         # Views Razor
│   ├── Services/                      # Serviços
│   ├── Data/                         # Contexto e migrations
│   └── wwwroot/                      # Arquivos estáticos
├── VillaBisutti.Delta.Tests/         # Projeto de testes
└── .github/workflows/                # GitHub Actions
```

## 📦 Módulos

### Eventos
- Cadastro e gerenciamento de eventos
- Associação com local, cardápio e tipo de serviço
- Controle de datas e horários
- Gestão de responsáveis

### Locais
- Cadastro de locais de evento
- Controle de capacidade
- Endereços e observações

### Cardápios
- Cadastro de cardápios
- Precificação
- Itens e descrições

### Tipos de Serviço
- Cadastro de tipos de serviço
- Configurações específicas por tipo

## 🔐 Autenticação

O sistema utiliza ASP.NET Core Identity para:
- Login com email e senha
- Recuperação de senha
- Alteração de senha
- Controle de sessão
- Proteção contra ataques

## 🧪 Testes

Execute os testes com:
```bash
dotnet test
```

Cobertura de testes:
- Controllers
- Services
- Regras de negócio

## 📈 CI/CD

Pipeline automatizado com GitHub Actions:
1. Build
2. Testes
3. Análise de código
4. Deploy para Azure

## 🔍 Logs e Monitoramento

- Logs estruturados com Serilog
- Monitoramento com Application Insights
- Métricas de performance
- Rastreamento de erros

## 🔄 Versionamento

Utilizamos [SemVer](http://semver.org/) para controle de versão.

## 👥 Autores

- **Delta** - *Desenvolvimento* - https://github.com/mahalverissimo-rgb/delta.git

## 📄 Licença

Este projeto está sob a licença MIT - veja o arquivo [LICENSE.md](LICENSE.md) para detalhes.
