# FutureWork API 

### Membros do Grupo:
Allan Von Ivanov - Rm98705

João Rodrigo Solano Nogueira - Rm551319

## 🚀 Sobre o Projeto

A **FutureWork API** é uma aplicação desenvolvida em **.NET 8** com suporte a **versionamento de API (API Versioning)**, **Entity Framework Core 8**, **Swagger**, e banco de dados **SQL Server (local)**. Ela oferece endpoints para gerenciamento de entidades como **Competências**, **Cargos**, **Candidatos**, entre outros.

Este documento explica como:

* Configurar o ambiente

* Executar as migrações

* Rodar o projeto

* Acessar o Swagger

* Entender a estrutura geral do projeto

---

## 📦 Tecnologias Utilizadas

* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core 8** (SqlServer, Tools, Design)
* **API Versioning 5.1.0**
* **Swagger / Swashbuckle 6.6.2**
* **SQL Server**

---

## ⚙️ Pré-requisitos

Antes de rodar, certifique-se de ter instalado:

* **.NET SDK 8.0**
* **SQL Server** (Developer, Express ou Docker)
* **SQL Server Management Studio (SSMS)** *(opcional, mas recomendado)*

## 🗃️ Criando e Atualizando o Banco de Dados (EF Core)

### Criar as migrações:

```
dotnet ef migrations add InitialCreate
```

### Aplicar as migrações:

```
dotnet ef database update
```

---

## ▶️ Rodando o Projeto

No diretório do projeto **FutureWork.API**, execute:

```
dotnet run
```

A API será iniciada normalmente, exemplo:

```
http://localhost:5188
```

---

## 📘 Acessando o Swagger (Documentação da API)

Acesse no navegador:

```
http://localhost:5188/swagger
```

Se estiver usando API Versioning, as rotas no Swagger aparecerão separadas por versão:

* `/swagger/v1/swagger.json`
* `/swagger/v2/swagger.json` *(se configurada)*

---

## 📚 Estrutura do Projeto

### Explicação rápida:

* **Controllers** → Contém endpoints divididos por versão.
* **Data** → Configuração do Entity Framework.
* **Models** → Classes representando entidades do banco.
* **Program.cs** → Configuração da aplicação.
* **Swagger** → Documentação automática da API.

---

## 🛠️ Funcionalidades Principais

* Cadastro e consulta de Competências
* Cadastro de Vagas
* Estrutura preparada para múltiplas versões de API
* Suporte a EF Core e migrações automáticas
* Documentação via Swagger
