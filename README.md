# S&P 500 Index Tracker (.NET Console App)

This project is a .NET console application that prints the latest S&P 500 index value from the database every 2 seconds, and fetches/updates the value from Yahoo Finance every 10 seconds. It uses Clean Architecture, Entity Framework Core for persistence, dependency injection, and robust async/await patterns. The S&P 500 value is scraped from Yahoo Finance using HtmlAgilityPack. Unit tests are provided using xUnit.

## Features
- Prints the latest S&P 500 index value from the database every 2 seconds
- Fetches and updates the S&P 500 value from Yahoo Finance every 10 seconds
- Stores each value in a local SQLite database
- Clean Architecture: Application, Domain, Infrastructure layers
- Repository pattern for data access
- Robust error handling and culture-aware parsing
- Unit tests with xUnit (see `Tests/`)

## How to Run

1. Build the project:
   ```powershell
   dotnet build
   ```
2. Run the application:
   ```powershell
   dotnet run
   ```
   The app will print the latest S&P 500 index value every 2 seconds and update it from Yahoo Finance every 10 seconds.

## How to Test

1. Navigate to the root directory (or `Tests` if running tests only):
   ```powershell
   dotnet test
   ```

## Project Structure
- Main app: `Program.cs`
- Application logic: `Application/`
- Domain models and interfaces: `Domain/`
- Infrastructure (data, services): `Infrastructure/`
- Unit tests: `Tests/`

---

*You can replace the index-fetching logic with your preferred data source if needed. The code follows Clean Architecture and repository pattern best practices.*
