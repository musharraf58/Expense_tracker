# ExpenseTracker API

A RESTful expense tracking API built with **ASP.NET Core 10**, **Entity Framework Core**, and **PostgreSQL**.

The project demonstrates CRUD operations, database relationships, validation, filtering, sorting, pagination, expense summaries, dependency injection, and Docker.

## Tech Stack

* C# / ASP.NET Core 10
* Entity Framework Core
* PostgreSQL
* Npgsql
* Docker & Docker Compose
* REST API
* OpenAPI

## Features

* Create, read, update, and delete expenses
* Create, read, update, and delete categories
* Expense-to-category relationships
* Input validation
* Filter expenses by:

  * Category
  * Date range
* Sort expenses by:

  * Amount
  * Date
  * Description
* Pagination
* Expense summaries:

  * Total amount
  * Number of expenses
  * Average expense
  * Spending by category
* PostgreSQL database
* Dockerized API and database

## API Endpoints

### Expenses

| Method | Endpoint                | Description         |
| ------ | ----------------------- | ------------------- |
| GET    | `/api/expenses`         | Get expenses        |
| GET    | `/api/expenses/{id}`    | Get one expense     |
| POST   | `/api/expenses`         | Create an expense   |
| PUT    | `/api/expenses/{id}`    | Update an expense   |
| DELETE | `/api/expenses/{id}`    | Delete an expense   |
| GET    | `/api/expenses/summary` | Get expense summary |

### Categories

| Method | Endpoint               | Description       |
| ------ | ---------------------- | ----------------- |
| GET    | `/api/categories`      | Get categories    |
| GET    | `/api/categories/{id}` | Get one category  |
| POST   | `/api/categories`      | Create a category |
| PUT    | `/api/categories/{id}` | Update a category |
| DELETE | `/api/categories/{id}` | Delete a category |

## Example Request

Create an expense:

```json
{
  "amount": 25.50,
  "description": "Lunch",
  "date": "2026-09-24T14:00:00Z",
  "categoryId": 1
}
```

## Filtering, Sorting & Pagination

Example:

```text
GET /api/expenses?categoryId=1&sortBy=amount&descending=true&page=1&pageSize=10
```

This retrieves the first 10 expenses in category `1`, sorted by amount from highest to lowest.

## Database

The application uses PostgreSQL with Entity Framework Core.

Database relationships:

```text
Category
   │
   │ 1
   │
   │ *
   ▼
Expense
```

Each expense belongs to one category, while a category can contain multiple expenses.

## Running Locally

### Prerequisites

* .NET 10 SDK
* PostgreSQL
* Docker (recommended)

### Using Docker

Clone the repository:

```bash
git clone https://github.com/musharraf58/Expense_tracker.git
cd ExpenseTracker
```

Start the application:

```bash
docker compose up --build
```

The API will be available at:

```text
http://localhost:8081
```

Stop the application:

```bash
docker compose down
```

## Project Structure

```text
ExpenseTracker/
├── Controllers/
│   ├── CategoriesController.cs
│   └── ExpensesController.cs
├── Data/
│   └── AppDbContext.cs
├── DTOs/
│   ├── ExpenseDto.cs
│   └── ExpenseSummaryDto.cs
├── Migrations/
├── Models/
│   ├── Category.cs
│   └── Expense.cs
├── Services/
│   ├── IExpenseService.cs
│   └── ExpenseService.cs
├── Dockerfile
├── docker-compose.yml
├── ExpenseTracker.csproj
├── Program.cs
└── README.md
```

## What I Practiced

This project was built to practice backend development with ASP.NET Core and includes:

* REST API design
* ASP.NET Core controllers
* Dependency Injection
* Entity Framework Core
* PostgreSQL
* Database migrations
* DTOs and validation
* Service-layer architecture
* Query filtering and sorting
* Pagination
* Aggregation and reporting
* Docker
* Git and GitHub

## Future Improvements

Possible future additions include:

* Authentication and authorization
* User-specific expenses
* Monthly spending reports
* Budget tracking
* Recurring expenses
* Frontend application
* Automated integration tests

## License

This project is for educational and portfolio purposes.
