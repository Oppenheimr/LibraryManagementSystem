# LibraryManagementSystem

![C#](https://img.shields.io/badge/language-C%23-239120) ![.NET](https://img.shields.io/badge/platform-.NET-512BD4?logo=dotnet)

A console-based library management application written in C#. Handles book cataloguing, member borrowing, and multi-level staff authentication — all through a clean menu-driven interface.

## Features

- **Book Management** — Add, update, delete, and search books (full CRUD)
- **Borrowing System** — Check out and return books, track due dates
- **Authentication** — Role-based access control with multiple access levels (admin, librarian, member)
- **Menu UI** — Intuitive numbered-menu navigation throughout the application
- **Singleton Pattern** — Database layer implemented as a Singleton for consistent state
- **Clean Architecture** — Clear separation between data models, database layer, business logic, and UI

## Tech Stack

| Layer | Technology |
|---|---|
| Language | C# |
| Platform | .NET 6+ (Console Application) |
| Pattern | Singleton (DB layer), layered architecture |

## Project Structure

```
LibraryManagementSystem/
├── Models/             # Book, Member, BorrowRecord data models
├── Database/           # Singleton data access layer
├── Extensions/         # Helper/extension methods
├── Menus/              # Menu classes for each feature area
├── Auth/               # Authentication and access-level logic
└── Program.cs          # Application entry point
```

## Getting Started

**Prerequisites**
- [.NET 6 SDK](https://dotnet.microsoft.com/download) or newer

**Running the project**
1. Clone the repository
   ```bash
   git clone https://github.com/Oppenheimr/LibraryManagementSystem.git
   ```
2. Navigate to the project directory
   ```bash
   cd LibraryManagementSystem
   ```
3. Build and run
   ```bash
   dotnet run
   ```

**Default credentials**

| Role | Username | Password |
|---|---|---|
| Admin | `admin` | `admin` |

> Change default credentials after first login.

## Author

**Umutcan Bağcı** — [github.com/Oppenheimr](https://github.com/Oppenheimr)

## License

This project is licensed under the [MIT License](LICENSE).
