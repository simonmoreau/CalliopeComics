---
description: 'Guidelines for building and maintaining C# applications with .NET 8
applyTo: '**/*.cs'
---

## Project Overview

CalliopeComics is an ASP.NET MCP Server application designed to manage comic book collections, including a comic book search engine and comic book files (crb and cbz) manipulations.

- **Backend:** ASP.NET REST API with a PostgreSQL database (`src\WebApp\WebApp.csproj`)
- **Testing:** Unit and integration tests are located in the `test/` directory

## C# Instructions
- Always use the latest C# features (currently C# 13).
- Write clear, concise, and expressive names for functions and classes, including their purpose and usage.
- Don't write comments, except for all public APIs.
- Use XML doc comments for all public APIs, including `<example>` and `<code>` tags where applicable.
- Always use the type instead of `var`.
- Do not use static for private methods.

## General Instructions
- Make only high confidence suggestions when reviewing code changes.
- Write code with good maintainability practices.
- Handle edge cases and write clear exception handling.
- For libraries or external dependencies, mention their usage and purpose in comments.
- When the project structure or practices change, update this file to keep Copilot suggestions relevant.
- Use dependency injection and MediatR for application logic.

## Project Structure
This solution uses Clean Architecture with separate projects for Domain, Application, Infrastructure, REST API.
- `src\WebApp\WebApp.csproj` – ASP.NET Core backend REST API.
- `src\Domain\Domain.csproj` – Application layer (business logic, services).
- `src\Infrastructure\Infrastructure.csproj` – Infrastructure layer (data access, external services).
- `test\Application.Test\Application.Test.csproj` – xUnit-based test project for the CalliopeComics.Application.

## Naming Conventions

- PascalCase for component names, method names, and public members.
- camelCase for private fields and local variables.
- Prefix interface names with "I" (e.g., `IUserService`).
- Prefix private field names with "_" (e.g., `_myField`).

## Formatting

- Insert a newline before the opening curly brace of any code block (e.g., after `if`, `for`, `while`, `foreach`, `using`, `try`, etc.).
- Ensure that the final return statement of a method is on its own line.
- Use pattern matching and switch expressions wherever possible.
- Use `nameof` instead of string literals when referring to member names.
- Favor returning early when checking inputs or preconditions, instead of using nested if statements or loops.

## Nullable Reference Types

- Declare variables non-nullable, and check for `null` at entry points.
- Always use `is null` or `is not null` instead of `== null` or `!= null`.
- Trust the C# null annotations and don't add null checks when the type system says a value cannot be null.

## Logging and Monitoring

- Guide the implementation of structured logging using Illogging.
- Explain the logging levels and when to use each.
- Demonstrate integration with Application Insights for telemetry collection.
- Show how to implement custom telemetry and correlation IDs for request tracking.
- Explain how to monitor API performance, errors, and usage patterns.

---

**Note:**  
As the project evolves, update this `copilot-instructions.md` file with new guidelines, patterns, or architectural changes to keep Copilot suggestions relevant and helpful.