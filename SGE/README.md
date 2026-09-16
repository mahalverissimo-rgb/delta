# SGE - Sistema de Gerenciamento de Eventos

Este é o sistema base do SGE, que serve como template para criar novas instâncias para cada cliente.

## Estrutura de Pastas

```
SGE/
├── CLIENTES/           # Pasta onde serão criadas as instâncias de cada cliente
│   └── [cliente-slug]/ # Uma pasta para cada cliente (criada automaticamente)
│       └── WebApp/     # Aplicação web do cliente
└── WebApp/             # Aplicação web base (template)
    ├── Controllers/    # Controladores da aplicação
    ├── Models/         # Modelos de dados
    ├── ViewModels/     # ViewModels
    ├── Views/          # Views da aplicação
    └── wwwroot/        # Arquivos estáticos
```

## Como Funciona

1. Quando um novo cliente é configurado através da página `/setup`:
   - Uma nova pasta é criada em `CLIENTES/[nome-do-cliente]`
   - Os arquivos necessários são copiados da pasta `WebApp` base
   - As configurações específicas do cliente são salvas em seu próprio `appsettings.json`
   - O logo do cliente é armazenado em sua própria pasta `wwwroot/uploads/logos`

2. Cada cliente tem sua própria instância independente do sistema, permitindo:
   - Configurações personalizadas
   - Temas próprios
   - Logos personalizados
   - Base de dados independente

## Configuração de Novo Cliente

1. Execute a aplicação base
2. Acesse `/setup`
3. Preencha as informações da empresa:
   - Nome completo
   - Nome curto (usado para criar a pasta do cliente)
   - Logo
   - Tema
4. Configure o usuário administrador inicial
5. O sistema criará automaticamente:
   - A pasta do cliente em `CLIENTES/[nome-curto]`
   - Uma cópia limpa do sistema
   - As configurações iniciais
   - O primeiro usuário administrador
