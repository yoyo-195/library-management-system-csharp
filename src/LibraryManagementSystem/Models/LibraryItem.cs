namespace LibraryManagementSystem.Models;

/// <summary>
/// Base abstract class that demonstrates inheritance and encapsulation.
/// Books, magazines, and DVDs inherit from this class.
/// </summary>
public abstract class LibraryItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string AuthorOrCreator { get; set; }
    public int Year { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; private set; } = true;
    public DateTime? DueDate { get; private set; }
    public int TimesBorrowed { get; private set; }

    protected LibraryItem(int id, string title, string authorOrCreator, int year, string category)
    {
        Id = id;
        Title = title;
        AuthorOrCreator = authorOrCreator;
        Year = year;
        Category = category;
    }

    public virtual void Borrow(int loanDays = 14)
    {
        if (!IsAvailable)
            throw new InvalidOperationException($"'{Title}' is already borrowed.");

        IsAvailable = false;
        DueDate = DateTime.Today.AddDays(loanDays);
        TimesBorrowed++;
    }

    public virtual void Return()
    {
        if (IsAvailable)
            throw new InvalidOperationException($"'{Title}' is already available.");

        IsAvailable = true;
        DueDate = null;
    }

    public bool IsOverdue() => !IsAvailable && DueDate.HasValue && DueDate.Value.Date < DateTime.Today;

    public abstract string GetItemType();

    public virtual string GetDetails()
    {
        string status = IsAvailable ? "Available" : $"Borrowed - Due {DueDate:yyyy-MM-dd}";
        return $"[{Id}] {GetItemType()} | {Title} | {AuthorOrCreator} | {Year} | {Category} | {status} | Borrowed {TimesBorrowed} time(s)";
    }
}
