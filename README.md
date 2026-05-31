# Library Management System — C# .NET Console App

A fully functional **C# .NET console application** built with **OOP principles**, **LINQ**, and a modular architecture. The project simulates a library system where users can manage books, magazines, DVDs, members, borrowing, returning, searching, filtering, and reporting.

## Features

- Object-Oriented Programming design
  - Encapsulation
  - Inheritance
  - Polymorphism
  - Abstraction
- CRUD operations for library items
- Member management
- Borrow and return workflow
- LINQ-based search and filtering
- Reports dashboard
  - Inventory by type
  - Inventory by category
  - Borrowed items
  - Overdue items
  - Most borrowed items
- Input validation
- Color-coded console messages
- Modular codebase with clear separation of concerns
- Seed data included for quick testing

## Tech Stack

- C#
- .NET 8
- OOP
- LINQ
- Console Application

## Project Structure

```text
library-management-system-csharp/
├── src/
│   └── LibraryManagementSystem/
│       ├── Data/
│       │   └── SeedData.cs
│       ├── Models/
│       │   ├── Book.cs
│       │   ├── Dvd.cs
│       │   ├── LibraryItem.cs
│       │   ├── Magazine.cs
│       │   └── Member.cs
│       ├── Services/
│       │   └── LibraryService.cs
│       ├── Utilities/
│       │   └── ConsoleHelper.cs
│       ├── Program.cs
│       └── LibraryManagementSystem.csproj
├── docs/
│   └── PROJECT_OVERVIEW.md
├── .gitignore
├── LICENSE
├── LibraryManagementSystem.sln
└── README.md
```

## How to Run

### 1. Clone the repository

```bash
git clone https://github.com/yoyo-195/library-management-system-csharp.git
cd library-management-system-csharp
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Run the application

```bash
dotnet run --project src/LibraryManagementSystem/LibraryManagementSystem.csproj
```

## Main Menu

```text
1. View all library items
2. Search items with LINQ
3. Filter by category
4. Add new book
5. Update book
6. Delete item
7. Borrow item
8. Return item
9. View members
10. Add member
11. Reports dashboard
0. Exit
```

## OOP Concepts Used

### Inheritance

`Book`, `Magazine`, and `Dvd` inherit from the abstract base class `LibraryItem`.

### Encapsulation

Item availability and due dates are controlled through methods like `Borrow()` and `Return()` instead of being changed directly from outside the class.

### Polymorphism

Each item type overrides `GetItemType()` and `GetDetails()` to provide item-specific behavior.

### Abstraction

`LibraryItem` defines shared behavior while forcing derived classes to implement item-specific details.

## LINQ Examples

The system uses LINQ for:

- Searching by title, creator, category, or item type
- Filtering by category
- Sorting results
- Grouping inventory by type and category
- Finding most borrowed items
- Finding overdue items

Example from `LibraryService.cs`:

```csharp
return _items
    .Where(i => i.Title.ToLower().Contains(keyword)
             || i.AuthorOrCreator.ToLower().Contains(keyword)
             || i.Category.ToLower().Contains(keyword)
             || i.GetItemType().ToLower().Contains(keyword))
    .OrderBy(i => i.Title)
    .ToList();
```


## Future Improvements

- Add JSON or SQLite persistence
- Add authentication and admin/member roles
- Add unit tests with xUnit
- Add fine calculation for overdue items
- Add export reports to CSV
- Build a GUI version using WinForms, WPF, or ASP.NET Core

## License

This project is licensed under the MIT License.
