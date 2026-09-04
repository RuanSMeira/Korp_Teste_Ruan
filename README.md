# Korp - Gestão de Estoque e Faturamento

Este repositório contém uma aplicação integrada para apoiar a rotina de uma empresa que precisa controlar produtos, acompanhar o estoque e emitir notas fiscais. O sistema foi construído como uma solução web em duas partes: uma interface Angular para a operação diária e uma API ASP.NET Core responsável pelos dados e pelas regras de negócio.

## Contexto do projeto

O sistema representa o ciclo operacional entre cadastro, estoque e faturamento:

1. A empresa é cadastrada e passa a ser o limite organizacional dos seus dados.
2. Usuários podem ser vinculados à empresa e acessar a área operacional.
3. Produtos são cadastrados com código, descrição e saldo inicial.
4. Entradas e saídas registram o histórico das alterações de estoque, incluindo saldo anterior e posterior.
5. Uma nota fiscal é criada com um ou mais produtos e começa aberta.
6. A emissão fecha a nota e efetiva a baixa dos itens no estoque, evitando que a mesma nota seja processada duas vezes.

Assim, o projeto não é apenas um cadastro de produtos: ele conecta o estoque ao faturamento e mantém rastreabilidade das movimentações realizadas. A empresa funciona como o contexto principal dos registros, enquanto o usuário associado identifica quem executou cada operação.

## Visão da solução

```text
Operador
   |
   v
Frontend Angular
   |  HTTP/JSON
   v
API ASP.NET Core
   |
   v
Entity Framework Core
   |
   v
SQL Server
```

### Frontend

O cliente web concentra a experiência de uso. Ele possui uma área pública, telas de autenticação e uma área protegida para as operações da empresa. Os fluxos disponíveis são:

- acesso de usuário ou empresa;
- cadastro de usuário ou empresa;
- painel da empresa;
- cadastro de produtos;
- consulta de saldo e indicadores de estoque;
- registro e consulta de movimentações;
- criação e emissão de notas fiscais.

Os guards de rota impedem o acesso à área interna sem sessão e restringem funções administrativas ao perfil de empresa. Os serviços de API traduzem as ações das telas para chamadas HTTP e exibem mensagens de validação retornadas pelo backend.

### Backend

A API é a camada que deve ser considerada como fonte das regras do sistema. Ela recebe os dados das telas ou de clientes externos, valida os contratos, aplica as regras de negócio e persiste as alterações. A separação entre controllers, services e repositories facilita distinguir:

- transporte HTTP e códigos de resposta;
- decisões do domínio, como disponibilidade de saldo e emissão;
- consultas e gravações no banco;
- entidades persistidas e contratos de entrada e saída.

### Banco de dados

O SQL Server armazena as empresas, usuários, produtos, notas fiscais, itens das notas e movimentações de estoque. O Entity Framework Core utiliza migrations para versionar a estrutura do banco e manter o schema alinhado ao modelo da aplicação.

## Principais regras do negócio

- Cada produto pertence a uma empresa e seu código deve ser único dentro dela.
- Uma nota fiscal precisa possuir pelo menos um item.
- Os produtos de uma nota devem pertencer à mesma empresa da nota.
- Uma nota aberta pode ser emitida uma única vez.
- A emissão baixa o estoque dos itens e não permite saldo negativo.
- Movimentações de estoque registram entrada ou saída e conservam o histórico do saldo.
- A atualização de produtos utiliza controle de concorrência para reduzir o risco de sobrescrever alterações feitas por outra operação.
- Empresas e usuários têm formas de acesso distintas, mas o projeto atual não utiliza JWT nem outro mecanismo de autenticação baseado em token.

## Organização do repositório

```text
Korp_Teste_Ruan/
├── Korp_Teste_Ruan_Backend/   API, regras, persistência e migrations
├── Korp_Teste_Ruan_Frontend/  Aplicação Angular e fluxos da interface
├── New Collection.postman_collection.json
├── Diagrama_DB_EMISSAONOTAFISCAL.png
└── documentação complementar em PDF
```

No backend, `Controllers`, `Services`, `Repositories`, `Models`, `DTOs` e `Data` refletem as responsabilidades da API. No frontend, `auth`, `core` e `dashboard` separam autenticação, infraestrutura compartilhada e os fluxos de operação.

## Como executar localmente

### 1. Preparar o banco

É necessário ter SQL Server ou LocalDB disponível. A connection string de desenvolvimento fica em:

`Korp_Teste_Ruan_Backend/appsettings.Development.json`

Confirme que o servidor e o banco configurados nessa string existem. Depois, no diretório do backend, aplique as migrations:

```bash
dotnet ef database update
```

### 2. Iniciar o backend

```bash
cd Korp_Teste_Ruan_Backend
dotnet restore
dotnet run
```

A API é configurada para responder em `http://localhost:5206` e `https://localhost:5207`.

### 3. Iniciar o frontend

Em outro terminal:

```bash
cd Korp_Teste_Ruan_Frontend
npm install
npm start
```

A aplicação web fica disponível em `http://localhost:4200` e utiliza o endpoint HTTP local do backend. O CORS da API já está preparado para essa origem.

## Verificações úteis

No frontend:

```bash
npm run build
npm test
```

No backend:

```bash
dotnet build
```

Para testar chamadas da API manualmente, o repositório também inclui uma collection do Postman na raiz.

## Documentação complementar

- [README do backend](Korp_Teste_Ruan_Backend/README.md): rotas, payloads, migrations e detalhes técnicos da API.
- [README do frontend](Korp_Teste_Ruan_Frontend/README.md): comandos específicos do Angular CLI.
- [Collection do Postman](New%20Collection.postman_collection.json): requisições para exercício e validação da API.
- [Diagrama do banco](Diagrama_DB_EMISSAONOTAFISCAL.png): visão visual das entidades persistidas.

## Pontos de atenção

- A API possui login e guards de navegação, mas ainda não há autenticação stateless com JWT. Em um ambiente produtivo, é necessário proteger as rotas no servidor e evitar confiar apenas na sessão mantida pelo frontend.
- As URLs locais e a connection string devem ser externalizadas por ambiente antes de um deploy.
- Alterações no modelo devem ser acompanhadas de uma nova migration revisada.
