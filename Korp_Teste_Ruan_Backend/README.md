# Korp Teste Ruan Backend

API REST para gerenciamento de empresas, usuários, produtos, estoque e notas fiscais. O projeto foi desenvolvido em ASP.NET Core com .NET 10, Entity Framework Core e SQL Server.

## Tecnologias

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server
- C# com nullable reference types habilitado
- Migrations para versionamento do banco de dados

## Pré-requisitos

- .NET SDK 10
- SQL Server LocalDB ou SQL Server instalado
- Banco acessível pela connection string configurada em `appsettings.json`
- Opcional: ferramenta do EF Core para executar migrations

## Configuração e execução

1. Ajuste `ConnectionStrings:DefaultConnection` em `appsettings.json` ou, preferencialmente, em `appsettings.Development.json`.
2. Aplique as migrations:

```bash
dotnet ef database update
```

3. Restaure as dependências e execute a API:

```bash
dotnet restore
dotnet run
```

Durante a execução local, a API fica disponível em:

- HTTP: `http://localhost:5206`
- HTTPS: `https://localhost:5207`

O CORS está configurado para permitir requisições de `http://localhost:4200`.

## Estrutura e padrões utilizados

```text
Controllers/       Entrada HTTP, rotas e códigos de resposta
DTOs/              Contratos de entrada e saída da API
Models/            Entidades persistidas e enums de domínio
Services/          Regras de negócio e orquestração dos casos de uso
Repositories/      Acesso e consultas ao banco de dados
Interfaces/        Contratos dos repositories
Data/              DbContext e configurações do EF Core
Migrations/        Histórico de alterações do schema do banco
Exceptions/        Exceções de domínio
```

Os principais padrões são:

- **Injeção de dependência:** repositories e services são registrados com ciclo de vida `Scoped` em `Program.cs`.
- **Repository:** os controllers não acessam o `DbContext` diretamente nos fluxos de empresas, usuários, produtos e notas fiscais.
- **Service Layer:** regras como unicidade de código, validação de saldo e emissão da nota fiscal ficam nos services.
- **DTOs:** requests controlam os dados aceitos pela API e responses evitam expor ciclos de navegação, especialmente nos itens da nota fiscal.
- **Data Annotations:** os DTOs usam `[Required]`, `[Range]`, `[EmailAddress]`, `[StringLength]` e `[MaxLength]` para validação automática.
- **EF Core Fluent Configuration:** os mapeamentos das entidades ficam em `Data/Configurations` e são carregados por `ApplyConfigurationsFromAssembly`.
- **Operações assíncronas:** acesso a dados e regras de negócio usam `Task` e métodos `Async`.
- **Enum como texto:** `JsonStringEnumConverter` está habilitado; por exemplo, `Tipo` aceita `Entrada` ou `Saida`.
- **Concorrência otimista:** produtos usam controle de concorrência no update para evitar sobrescritas silenciosas.
- **Transação de estoque:** uma movimentação atualiza o saldo e registra o histórico na mesma transação.

### Regras de negócio relevantes

- CNPJ é normalizado para somente dígitos no cadastro da empresa.
- E-mails de usuários são normalizados para minúsculas no cadastro.
- O código do produto deve ser único dentro da empresa.
- Uma nota fiscal precisa ter pelo menos um item e seus produtos devem pertencer à empresa informada.
- A nota nasce com status `Aberta`. Ao emitir, passa para `Fechada` e o estoque é debitado uma única vez por produto.
- Não é possível emitir uma nota sem itens, emitir uma nota já fechada ou deixar o estoque negativo.
- Login de empresa e usuário retorna os dados do perfil, mas o projeto atualmente não configura JWT ou outro mecanismo de autenticação.

## Rotas disponíveis

Todas as rotas abaixo usam o prefixo `/api`. Os nomes seguem os atributos `[Route]` dos controllers; a aplicação não força pluralização automática.

### Empresas

| Método | Rota | Descrição | Corpo |
| --- | --- | --- | --- |
| GET | `/api/Empresa` | Lista empresas | - |
| GET | `/api/Empresa/{id}` | Busca uma empresa | - |
| POST | `/api/Empresa` | Cria uma empresa | `CriarEmpresaRequest` |
| POST | `/api/Empresa/login` | Autentica uma empresa | `LoginEmpresaRequest` |
| PUT | `/api/Empresa/{id}` | Atualiza uma empresa | Entidade `Empresa` |
| DELETE | `/api/Empresa/{id}` | Remove uma empresa | - |

### Usuários

| Método | Rota | Descrição | Corpo |
| --- | --- | --- | --- |
| POST | `/api/Usuario/login` | Autentica um usuário | `LoginRequest` |
| GET | `/api/Usuario` | Lista usuários | - |
| GET | `/api/Usuario/empresa/{empresaId}` | Lista usuários da empresa | - |
| GET | `/api/Usuario/{id}` | Busca um usuário | - |
| POST | `/api/Usuario` | Cria um usuário | `CriarUsuarioRequest` |
| PUT | `/api/Usuario/{id}` | Atualiza um usuário | Entidade `Usuario` |
| DELETE | `/api/Usuario/{id}` | Remove um usuário | - |

### Produtos e saldo

| Método | Rota | Descrição | Corpo |
| --- | --- | --- | --- |
| GET | `/api/Produtos` | Lista produtos | - |
| GET | `/api/Produtos/empresa/{empresaId}` | Lista produtos da empresa | - |
| GET | `/api/Produtos/empresa/{empresaId}/saldo` | Retorna resumo do estoque | - |
| GET | `/api/Produtos/{id}` | Busca um produto | - |
| POST | `/api/Produtos` | Cria um produto | `CriarProdutoRequest` |
| PUT | `/api/Produtos/{id}` | Atualiza um produto | Entidade `Produto` |
| DELETE | `/api/Produtos/{id}` | Remove um produto | - |

O resumo de saldo contém `TotalProdutos`, `SaldoTotal`, `ProdutosBaixoEstoque`, `ProdutosSemEstoque` e a lista `Produtos`. Considera-se baixo estoque um saldo maior que zero e menor que 10.

### Notas fiscais

| Método | Rota | Descrição | Corpo |
| --- | --- | --- | --- |
| POST | `/api/NotaFiscal` | Cria uma nota com seus itens | `CriarNotaFiscalRequest` |
| GET | `/api/NotaFiscal/empresa/{empresaId}` | Lista notas da empresa | - |
| GET | `/api/NotaFiscal/{id}` | Busca uma nota | - |
| PUT | `/api/NotaFiscal/{id}/emitir` | Emite a nota e baixa o estoque | - |
| POST | `/api/notafiscal/{notaFiscalId}/itens` | Adiciona item a uma nota | `AdicionarItemRequest` |

### Movimentações de estoque

| Método | Rota | Descrição | Corpo |
| --- | --- | --- | --- |
| GET | `/api/movimentacoesestoque/empresa/{empresaId}` | Lista movimentações da empresa | - |
| POST | `/api/movimentacoesestoque` | Registra entrada ou saída | `CriarMovimentacaoEstoqueRequest` |

## Exemplos de requisição

### Criar empresa

```http
POST /api/Empresa
Content-Type: application/json

{
  "razaoSocial": "Empresa Exemplo LTDA",
  "nomeFantasia": "Empresa Exemplo",
  "cnpj": "12345678000199",
  "senhaMaster": "senha-segura"
}
```

### Criar produto

```http
POST /api/Produtos
Content-Type: application/json

{
  "empresaId": 1,
  "codigo": "PROD-001",
  "descricao": "Produto de exemplo",
  "saldoInicial": 100
}
```

### Criar nota fiscal com itens

```http
POST /api/NotaFiscal
Content-Type: application/json

{
  "empresaId": 1,
  "usuarioEmissorId": 1,
  "itens": [
    {
      "produtoId": 1,
      "quantidade": 2
    }
  ]
}
```

### Registrar movimentação de estoque

```http
POST /api/movimentacoesestoque
Content-Type: application/json

{
  "empresaId": 1,
  "produtoId": 1,
  "usuarioId": 1,
  "tipo": "Entrada",
  "quantidade": 10,
  "observacao": "Reposição de estoque"
}
```

## Respostas e erros

- `200 OK`: consulta ou operação concluída.
- `201 Created`: empresa, usuário ou produto criado com sucesso.
- `204 No Content`: exclusão concluída.
- `400 Bad Request`: payload inválido ou regra de negócio violada.
- `401 Unauthorized`: credenciais inválidas.
- `404 Not Found`: recurso, empresa, produto ou usuário não encontrado.
- `409 Conflict`: conflito de unicidade ou operação incompatível.

Erros de validação do model binding são retornados pelo `ApiController`. Regras de negócio normalmente retornam uma mensagem em `erro` ou diretamente como texto, conforme o controller.

## Migrations

Para criar uma nova migration após alterar os modelos:

```bash
dotnet ef migrations add NomeDaMigration
dotnet ef database update
```

As migrations existentes ficam em `Migrations/` e devem ser revisadas junto com as alterações de domínio.