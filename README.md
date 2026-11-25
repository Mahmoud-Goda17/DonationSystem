# DonationSystem

Blood donation management REST API: donors, donation requests, donation history and notification logs.

## Features

- Donors: registration with blood type, availability checks
- Donation requests: create and track requests, match available donors, status transitions guarded by domain exceptions
- Donation history per donor
- Notification log entries
- Auth: JWT access tokens with refresh tokens (ASP.NET Core Identity)
- Global error-handling middleware, unit of work + generic repository, EF Core configurations and migrations

## Tech

.NET 8, ASP.NET Core, Entity Framework Core, SQL Server, ASP.NET Core Identity + JWT, Swagger

## Running locally

1. Prerequisites: .NET 8 SDK, SQL Server.
2. Set the connection string and JWT options with `dotnet user-secrets`.
3. `dotnet ef database update`, then run `DonationSystem.API`.
