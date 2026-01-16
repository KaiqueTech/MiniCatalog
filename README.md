# Mini Hub de Catálogo

API REST em **.NET** para gerenciamento de um catálogo de itens (produtos/serviços), com **ASP.NET Identity**, **EF Core**, **JWT**, **consultas avançadas com LINQ**, **importação/exportação de dados**, e **auditoria em NoSQL (MongoDB via Docker)**.

Este projeto foi desenvolvido como atividade prática para consolidar conceitos de **arquitetura em camadas**, **segurança**, **persistência**, **integrações** e **boas práticas**.

---

## 🧱 Arquitetura

O projeto segue uma separação clara de responsabilidades:

```
MiniCatalog
├── MiniCatalog.Api           # Controllers, Middlewares, Configurações
├── MiniCatalog.Application   # Services, DTOs, Interfaces, Validators
├── MiniCatalog.Domain        # Entidades, Enums, Constantes
├── MiniCatalog.Infra         # EF Core, Identity, Repositórios, Migrations
```

---

## 🔐 Autenticação e Autorização

Implementado com **ASP.NET Identity + JWT**.

### Roles

* **Admin** → acesso total
* **Editor** → cria e edita itens
* **Viewer** → somente leitura

As permissões são aplicadas via **Policies**.

### Endpoints de Auth

| Método | Rota             | Descrição                            |
| ------ | ---------------- | ------------------------------------ |
| POST   | `/auth/register` | Registro de usuário                  |
| POST   | `/auth/login`    | Login e geração do JWT               |
| GET    | `/me`            | Retorna dados do usuário autenticado |

---

## 🗄️ Persistência Relacional (EF Core)

Banco relacional com **EF Core + Migrations**.

Padronização de Entidades (BaseModels)
Para garantir consistência em todo o domínio, todas as entidades e logs herdam de classes base que automatizam o controle de metadados.

1. BaseModel
   Utilizada por todas as entidades de negócio persistidas no SQL Server (Ex: Item, Categoria).

Propriedades:

Id (Guid): Identificador único universal.

CreatedAt (DateTime): Data de criação (UTC) definida automaticamente no construtor.

UpdatedAt (DateTime?): Data da última modificação.

Comportamento:

Método SetUpdated(): Atualiza a propriedade UpdatedAt com o timestamp atual sempre que uma alteração é realizada na entidade.

2. BaseLogModel
   Utilizada para documentos de auditoria e logs (MongoDB ou Fallback JSON).

Propriedades:

Id (Guid): Identificador do log.

Timestamp (DateTime): Registro preciso de quando o evento ocorreu.

### Entidade principal: Item

* `Id (Guid)`
* `Nome`
* `Descricao`
* `Categoria`
* `Preco`
* `Ativo`
* `Tags` (tabela relacionada `ItemTag`)
* `CreatedAt / UpdatedAt`

### Observações de modelagem

* Tags foram normalizadas em tabela própria para permitir **busca eficiente** e **flexibilidade**.
* Índices criados para campos relevantes (`Nome`, `Categoria`).
* Seed inicial com categorias,itens e tags.

---

## 🔎 Busca Avançada com LINQ

Endpoint:

```
GET /items/search
```

### Parâmetros suportados

* `term`
* `categoria`
* `min` / `max` (preço)
* `ativo`
* `tags`
* `sort` (nome, preço, data)
* `page`
* `pageSize`

### Recursos implementados

* Filtros combináveis
* Ordenação dinâmica
* Paginação
* Agregações:

    * Total de itens encontrados
    * Média de preço

---

## 🌐 Importação via API Externa

Endpoint:

```
POST /items/import
```

Funcionalidades:

* Consumo de API externa (ex.: Mock/Fake API)
* Mapeamento para entidade `Item`
* Persistência no banco
* **Deduplicação** por Nome + Categoria

---

## 📄 Exportação de Relatórios

Endpoint:

```
GET /reports/items
```

Gera arquivo **CSV ou JSON** contendo:

* Itens ativos
* Quantidade por categoria
* Média de preços
* Top 3 itens mais caros

O arquivo é gerado no servidor e retornado para download.

---

## 🧾 Auditoria (NoSQL)

Auditoria de ações importantes utilizando **MongoDB (Docker)**.

### Ações auditadas

* Login bem-sucedido
* Criação / edição / exclusão de item
* Importação
* Exportação

### Modelo do log

* `Id`
* `Action`
* `UserId`
* `Timestamp`
* `Payload`

### Observação

Caso MongoDB não esteja disponível, pode ser utilizado fallback em arquivo `audit_logs.json` mantendo o mesmo modelo.

---

## 🐳 Docker

O projeto utiliza **Docker Compose** para subir o MongoDB.

### Subir serviços

```bash
docker-compose up -d
```

---

## ▶️ Como Rodar o Projeto

### Pré-requisitos

* .NET 8+
* Docker
* SQL Server (local ou container)
* MongoDb (Atlas, local ou container)

### Passos

```bash
# restaurar dependências
dotnet restore

# aplicar migrations
dotnet ef database update -p MiniCatalog.Infra -s MiniCatalog.Api

# rodar a aplicação
dotnet run --project MiniCatalog.Api
```

---

## 🔑 Variáveis de Ambiente (exemplo)

```env
ConnectionStrings__DefaultConnection=Server=localhost,1433;Database=DatabaseName;User Id=sa;Password=password;TrustServerCertificate=True;
JwtSettings__Secret=Secret_Key
JwtSettings__Issuer=Issuer
JwtSettings__Audience=Audience
MongoSettings__ConnectionString=mongodb://usuario:senha@localhost:27017
MongoSettings__Database=Database
```

---

## 📌 Critérios Atendidos

* ✅ Identity + Roles + JWT
* ✅ EF Core + Migrations + Seed
* ✅ LINQ avançado com paginação, ordenação e agregação
* ✅ Importação via API externa sem duplicação de itens
* ✅ Exportação de relatório em arquivo .csv
* ✅ Auditoria com NoSQL (MongoDB)

---

## 📎 Observações Finais

Projeto desenvolvido com foco em **boas práticas**, **clareza arquitetural** e **organização de código**, simulando um cenário real de API corporativa.

---

👨‍💻 Autor: *Kaique Bezerra da Silva*