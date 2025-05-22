# S&P 500 Index Tracker (.NET Console App)

This project is a .NET console application that prints the latest S&P 500 index value from the database every 2 seconds, and fetches/updates the value from Yahoo Finance every 10 seconds. It uses Clean Architecture, Entity Framework Core for persistence, dependency injection, and robust async/await patterns. The S&P 500 value is scraped from Yahoo Finance using HtmlAgilityPack.

## Features
- Prints the latest S&P 500 index value from the database every 2 seconds
- Fetches and updates the S&P 500 value from Yahoo Finance every 10 seconds
- Stores each value in a local SQLite database
- Clean Architecture: Application, Domain, Infrastructure layers
- Repository pattern for data access
- Robust error handling and culture-aware parsing

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Internet connection (for fetching S&P 500 values)

### Run
1. Restore dependencies and build the project:
   ```powershell
   dotnet restore
   dotnet build
   ```
2. Apply database migrations (creates the SQLite database and tables):
   ```powershell
   dotnet ef database update
   ```
3. Run the application:
   ```powershell
   dotnet run
   ```
   The app will print the latest S&P 500 index value every 2 seconds and update it from Yahoo Finance every 10 seconds.

## Testing

There are currently **no unit tests** in this repository. You can add your own test project using xUnit or your preferred framework.

## Project Structure
- Main app: `Program.cs`
- Application logic: `IndexTracker.Application/`
- Domain models and interfaces: `IndexTracker.Domain/`
- Infrastructure (data, services): `IndexTracker.Infrastructure/`
- Database migrations: `Migrations/`

---

*You can replace the index-fetching logic with your preferred data source if needed. The code follows Clean Architecture and repository pattern best practices.*
