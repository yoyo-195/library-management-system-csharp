using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class LibraryService
{
    private readonly List<LibraryItem> _items;
    private readonly List<Member> _members;

    public LibraryService(List<LibraryItem> items, List<Member> members)
    {
        _items = items;
        _members = members;
    }

    public IReadOnlyList<LibraryItem> GetAllItems() => _items;
    public IReadOnlyList<Member> GetAllMembers() => _members;

    public void AddItem(LibraryItem item) => _items.Add(item);

    public bool DeleteItem(int itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item is null || !item.IsAvailable) return false;
        _items.Remove(item);
        return true;
    }

    public bool UpdateBook(int id, string title, string author, int year, string category, string isbn, int pages)
    {
        var book = _items.OfType<Book>().FirstOrDefault(b => b.Id == id);
        if (book is null) return false;
        book.Title = title;
        book.AuthorOrCreator = author;
        book.Year = year;
        book.Category = category;
        book.Isbn = isbn;
        book.PageCount = pages;
        return true;
    }

    public List<LibraryItem> Search(string keyword)
    {
        keyword = keyword.Trim().ToLower();
        return _items
            .Where(i => i.Title.ToLower().Contains(keyword)
                     || i.AuthorOrCreator.ToLower().Contains(keyword)
                     || i.Category.ToLower().Contains(keyword)
                     || i.GetItemType().ToLower().Contains(keyword))
            .OrderBy(i => i.Title)
            .ToList();
    }

    public List<LibraryItem> FilterByCategory(string category) => _items
        .Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
        .OrderBy(i => i.Title)
        .ToList();

    public List<LibraryItem> GetAvailableItems() => _items.Where(i => i.IsAvailable).OrderBy(i => i.Title).ToList();

    public List<LibraryItem> GetBorrowedItems() => _items.Where(i => !i.IsAvailable).OrderBy(i => i.DueDate).ToList();

    public List<LibraryItem> GetOverdueItems() => _items.Where(i => i.IsOverdue()).OrderBy(i => i.DueDate).ToList();

    public List<LibraryItem> GetMostBorrowedItems(int count = 5) => _items
        .OrderByDescending(i => i.TimesBorrowed)
        .ThenBy(i => i.Title)
        .Take(count)
        .ToList();

    public bool AddMember(Member member)
    {
        if (_members.Any(m => m.Email.Equals(member.Email, StringComparison.OrdinalIgnoreCase))) return false;
        _members.Add(member);
        return true;
    }

    public bool BorrowItem(int itemId, int memberId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        var member = _members.FirstOrDefault(m => m.Id == memberId);
        if (item is null || member is null || !item.IsAvailable) return false;

        item.Borrow();
        member.BorrowedItemIds.Add(itemId);
        return true;
    }

    public bool ReturnItem(int itemId, int memberId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        var member = _members.FirstOrDefault(m => m.Id == memberId);
        if (item is null || member is null || item.IsAvailable || !member.BorrowedItemIds.Contains(itemId)) return false;

        item.Return();
        member.BorrowedItemIds.Remove(itemId);
        return true;
    }

    public Dictionary<string, int> GetInventoryByType() => _items
        .GroupBy(i => i.GetItemType())
        .ToDictionary(g => g.Key, g => g.Count());

    public Dictionary<string, int> GetInventoryByCategory() => _items
        .GroupBy(i => i.Category)
        .OrderByDescending(g => g.Count())
        .ToDictionary(g => g.Key, g => g.Count());

    public int GenerateNextItemId() => _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
    public int GenerateNextMemberId() => _members.Any() ? _members.Max(m => m.Id) + 1 : 1;
}
