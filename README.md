# Summer 2026 further study of C#

# Activity Manager

A small C# console application for managing activities. It loads activities from a JSON file, lets the user add a new activity, and saves the updated list.

## Requirements

- .NET 10 SDK

## Run the application

From the repository root:

```powershell
dotnet run --project .\GestoreAttivita\GestoreAttivita.csproj
```

The application asks for a title, an optional description, a due date, and a priority. Activities are persisted in the JSON file configured in `appsettings.json`.

## Main features

- Validates required titles, due dates, states, and priorities.
- Prevents duplicate titles, ignoring case and leading or trailing spaces.
- Loads and saves activities asynchronously through a JSON repository.
- Writes informative console logs for loading, adding, and saving activities.

## Run the tests

```powershell
dotnet test .\GestoreAttivita.Tests\GestoreAttivita.Tests.csproj
```

The test suite covers a valid activity, an invalid empty title, and duplicate-title validation.

## Project structure

- `GestoreAttivita`: console application, domain model, service, and JSON repository.
- `GestoreAttivita.Tests`: xUnit unit tests and an in-memory fake repository.
