using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Utilities;

var libraryService = new LibraryService(SeedData.GetLibraryItems(), SeedData.GetMembers());

while (true)
{
    ConsoleHelper.Header("Library Management System - C# .NET Console App");
    Console.WriteLine("1. View all library items");
    Console.WriteLine("2. Search items with LINQ");
    Console.WriteLine("3. Filter by category");
    Console.WriteLine("4. Add new book");
    Console.WriteLine("5. Update book");
    Console.WriteLine("6. Delete item");
    Console.WriteLine("7. Borrow item");
    Console.WriteLine("8. Return item");
    Console.WriteLine("9. View members");
    Console.WriteLine("10. Add member");
    Console.WriteLine("11. Reports dashboard");
    Console.WriteLine("0. Exit");

    int choice = ConsoleHelper.ReadInt("\nChoose an option: ", 0, 11);

    switch (choice)
    {
        case 1: ViewAllItems(); break;
        case 2: SearchItems(); break;
        case 3: FilterByCategory(); break;
        case 4: AddBook(); break;
        case 5: UpdateBook(); break;
        case 6: DeleteItem(); break;
        case 7: BorrowItem(); break;
        case 8: ReturnItem(); break;
        case 9: ViewMembers(); break;
        case 10: AddMember(); break;
        case 11: ReportsDashboard(); break;
        case 0: return;
    }
}

void ViewAllItems()
{
    ConsoleHelper.Header("All Library Items");
    foreach (var item in libraryService.GetAllItems())
        Console.WriteLine(item.GetDetails());
    ConsoleHelper.Pause();
}

void SearchItems()
{
    ConsoleHelper.Header("Search Items");
    string keyword = ConsoleHelper.ReadRequiredString("Enter title, author, category, or type: ");
    var results = libraryService.Search(keyword);
    PrintItems(results, "No matching items found.");
}

void FilterByCategory()
{
    ConsoleHelper.Header("Filter by Category");
    string category = ConsoleHelper.ReadRequiredString("Enter category: ");
    var results = libraryService.FilterByCategory(category);
    PrintItems(results, "No items found in this category.");
}

void AddBook()
{
    ConsoleHelper.Header("Add New Book");
    int id = libraryService.GenerateNextItemId();
    string title = ConsoleHelper.ReadRequiredString("Title: ");
    string author = ConsoleHelper.ReadRequiredString("Author: ");
    int year = ConsoleHelper.ReadInt("Year: ", 1000, DateTime.Today.Year);
    string category = ConsoleHelper.ReadRequiredString("Category: ");
    string isbn = ConsoleHelper.ReadRequiredString("ISBN: ");
    int pages = ConsoleHelper.ReadInt("Page count: ", 1, 10000);

    libraryService.AddItem(new Book(id, title, author, year, category, isbn, pages));
    ConsoleHelper.Success("Book added successfully.");
    ConsoleHelper.Pause();
}

void UpdateBook()
{
    ConsoleHelper.Header("Update Book");
    int id = ConsoleHelper.ReadInt("Book ID: ", 1);
    string title = ConsoleHelper.ReadRequiredString("New title: ");
    string author = ConsoleHelper.ReadRequiredString("New author: ");
    int year = ConsoleHelper.ReadInt("New year: ", 1000, DateTime.Today.Year);
    string category = ConsoleHelper.ReadRequiredString("New category: ");
    string isbn = ConsoleHelper.ReadRequiredString("New ISBN: ");
    int pages = ConsoleHelper.ReadInt("New page count: ", 1, 10000);

    bool updated = libraryService.UpdateBook(id, title, author, year, category, isbn, pages);
    if (updated) ConsoleHelper.Success("Book updated successfully.");
    else ConsoleHelper.Error("Book not found.");
    ConsoleHelper.Pause();
}

void DeleteItem()
{
    ConsoleHelper.Header("Delete Item");
    int id = ConsoleHelper.ReadInt("Item ID: ", 1);
    bool deleted = libraryService.DeleteItem(id);
    if (deleted) ConsoleHelper.Success("Item deleted successfully.");
    else ConsoleHelper.Error("Item not found or currently borrowed.");
    ConsoleHelper.Pause();
}

void BorrowItem()
{
    ConsoleHelper.Header("Borrow Item");
    int itemId = ConsoleHelper.ReadInt("Item ID: ", 1);
    int memberId = ConsoleHelper.ReadInt("Member ID: ", 1);
    bool borrowed = libraryService.BorrowItem(itemId, memberId);
    if (borrowed) ConsoleHelper.Success("Item borrowed successfully.");
    else ConsoleHelper.Error("Borrow failed. Check item availability and member ID.");
    ConsoleHelper.Pause();
}

void ReturnItem()
{
    ConsoleHelper.Header("Return Item");
    int itemId = ConsoleHelper.ReadInt("Item ID: ", 1);
    int memberId = ConsoleHelper.ReadInt("Member ID: ", 1);
    bool returned = libraryService.ReturnItem(itemId, memberId);
    if (returned) ConsoleHelper.Success("Item returned successfully.");
    else ConsoleHelper.Error("Return failed. Check item ID, member ID, and borrowing record.");
    ConsoleHelper.Pause();
}

void ViewMembers()
{
    ConsoleHelper.Header("Members");
    foreach (var member in libraryService.GetAllMembers())
        Console.WriteLine(member.GetDetails());
    ConsoleHelper.Pause();
}

void AddMember()
{
    ConsoleHelper.Header("Add Member");
    int id = libraryService.GenerateNextMemberId();
    string name = ConsoleHelper.ReadRequiredString("Full name: ");
    string email = ConsoleHelper.ReadRequiredString("Email: ");
    bool added = libraryService.AddMember(new Member(id, name, email));
    if (added) ConsoleHelper.Success("Member added successfully.");
    else ConsoleHelper.Error("A member with this email already exists.");
    ConsoleHelper.Pause();
}

void ReportsDashboard()
{
    ConsoleHelper.Header("Reports Dashboard");

    Console.WriteLine("Inventory by Type:");
    foreach (var pair in libraryService.GetInventoryByType())
        Console.WriteLine($"- {pair.Key}: {pair.Value}");

    Console.WriteLine("\nInventory by Category:");
    foreach (var pair in libraryService.GetInventoryByCategory())
        Console.WriteLine($"- {pair.Key}: {pair.Value}");

    Console.WriteLine("\nCurrently Borrowed Items:");
    PrintItemsInline(libraryService.GetBorrowedItems(), "- None");

    Console.WriteLine("\nOverdue Items:");
    PrintItemsInline(libraryService.GetOverdueItems(), "- None");

    Console.WriteLine("\nMost Borrowed Items:");
    PrintItemsInline(libraryService.GetMostBorrowedItems(), "- No borrowing activity yet");

    ConsoleHelper.Pause();
}

void PrintItems(List<LibraryItem> items, string emptyMessage)
{
    Console.WriteLine();
    PrintItemsInline(items, emptyMessage);
    ConsoleHelper.Pause();
}

void PrintItemsInline(List<LibraryItem> items, string emptyMessage)
{
    if (!items.Any())
    {
        Console.WriteLine(emptyMessage);
        return;
    }

    foreach (var item in items)
        Console.WriteLine(item.GetDetails());
}
