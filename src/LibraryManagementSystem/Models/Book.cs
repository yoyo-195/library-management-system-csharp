namespace LibraryManagementSystem.Models;

public class Book : LibraryItem
{
    public string Isbn { get; set; }
    public int PageCount { get; set; }

    public Book(int id, string title, string author, int year, string category, string isbn, int pageCount)
        : base(id, title, author, year, category)
    {
        Isbn = isbn;
        PageCount = pageCount;
    }

    public override string GetItemType() => "Book";

    public override string GetDetails()
    {
        return base.GetDetails() + $" | ISBN: {Isbn} | Pages: {PageCount}";
    }
}
