# OrderManagementApi-TestTask

## How to set up project

First of all change connection string to database in `appsetting.json` file.

To set up the project you can use Docker or just run in terminal `dotnet run`.

## Task

Objective: Create a simple RESTful Web API using ASP.NET Core 6.0 that manages customer orders and
utilizes Azure Service Bus to notify when a new order is created.
Requirements:
1. Database:
- Use either SQL Server or PostgreSQL.
- Tables: Customers, Orders. Define necessary fields based on your understanding.
- Use entity framework code-first approach or in-memory database
2. API Endpoints:
- `POST /orders`: To create a new order for a customer.
- `GET /orders`: To retrieve a list of all orders.
- `GET /orders/{orderId}`: To retrieve details of a specific order.
- `GET /customers`: To retrieve a list of all customers.
- `POST /customers`: To create a new customer.
3. Service Bus Integration:
- When a new order is created, send a message to an Azure Service Bus Topic.
- Consumer gets the message and updates Customer orders count
- For simplicity, you can use Azure Service Bus's free tier.
4. Unit Tests:
- Write unit tests for at least two of the main business logic methods.
5. Documentation:
- Include a `README.md` in your GitHub repository that:
- Explains how to set up and run your application.
- Gives a brief overview of the architectural decisions you made.
6. Bonus (optional but will be a plus):
- Add a simple `PATCH /orders/{orderId}` endpoint to update the status of an order.
- Containerize the application using Docker.

Guidelines:
- Prioritize code readability, simplicity, and maintainability over performance optimizations.
- Consider error scenarios and handle them gracefully.
- Feel free to use any additional NuGet packages if you feel they're appropriate.
Submission:
- Push your solution to a public GitHub repository.
- Share the link to the repository for review.
- Ensure the solution can be run easily after cloning or provide specific setup instructions.