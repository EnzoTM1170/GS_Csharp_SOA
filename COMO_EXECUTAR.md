# Como Executar o Projeto MindCare

## Pré-requisitos

- Visual Studio 2022 ou superior
- .NET SDK 8.0
- Nenhum banco externo (SQLite embarcado já incluso)

## Execução via Visual Studio (Recomendado)

### 1. Abrir o Projeto
1. Abra o Visual Studio 2022
2. Abra o arquivo `MindCare.sln`
3. Aguarde a restauração dos pacotes NuGet

### 2. Executar a API
1. No Solution Explorer, clique com botão direito em `MindCare.API`
2. Selecione "Set as Startup Project"
3. Pressione F5 ou clique em "Run"
4. A API será iniciada em `http://localhost:5000`
5. O Swagger estará disponível em `http://localhost:5000/swagger`

### 3. Executar o WPF
1. **Mantenha a API rodando** (não feche a janela)
2. No Solution Explorer, clique com botão direito em `MindCare.WPF`
3. Selecione "Set as Startup Project"
4. Pressione F5 ou clique em "Run"
5. A aplicação WPF será aberta

## Execução via Linha de Comando

### 1. Restaurar e Compilar
```bash
dotnet restore MindCare.sln
dotnet build MindCare.sln
```

### 2. Executar a API (Terminal 1)
```bash
cd MindCare.API
dotnet run
```

**IMPORTANTE:** Aguarde até ver as mensagens:
- `Now listening on: http://localhost:5000`
- `✅ Banco de dados populado com sucesso!`

A API estará disponível em: `http://localhost:5000`

### 3. Executar o WPF (Terminal 2 - Novo Terminal)
**Só execute o WPF DEPOIS que a API estiver rodando!**

```bash
cd MindCare.WPF
dotnet run
```

## Banco de Dados

O banco de dados será criado automaticamente na primeira execução da API. 
Dados de exemplo serão populados automaticamente incluindo:
- 9 funcionários com diferentes níveis de estresse
- Métricas de saúde dos últimos 7 dias
- Análises emocionais
- Alertas de estresse

## Verificação

### API está funcionando?
1. Abra o navegador em: `http://localhost:5000/swagger`
2. Você deve ver a documentação da API

### WPF está funcionando?
1. A aplicação deve abrir automaticamente
2. Você deve ver a tela de Dashboard com dados populados

## Solução de Problemas

### Erro: "dotnet não é reconhecido"
- Instale o .NET SDK 8.0: https://dotnet.microsoft.com/download/dotnet/8.0
- Ou use o Visual Studio que já inclui o SDK

### Erro: "Porta já em uso"
- Feche outros processos usando a porta 5000
- Ou altere a porta em `MindCare.API/Properties/launchSettings.json`

### Erro: "Não foi possível conectar à API"
1. Verifique se a API está rodando
2. Verifique a URL em `MindCare.WPF/Services/ApiService.cs`
3. Por padrão: `http://localhost:5000/api`

### Erro de Certificado SSL
- Sem necessidade quando usar HTTP (porta 5000). Se habilitar HTTPS, aceite o certificado.
