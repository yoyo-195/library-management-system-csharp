using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data;

public static class SeedData
{
    public static List<LibraryItem> GetLibraryItems() => new()
    {
        new Book(1, "Clean Code", "Robert C. Martin", 2008, "Programming", "9780132350884", 464),
        new Book(2, "The Pragmatic Programmer", "Andrew Hunt", 1999, "Programming", "9780201616224", 352),
        new Book(3, "C# in Depth", "Jon Skeet", 2019, "Programming", "9781617294532", 528),
        new Book(4, "Atomic Habits", "James Clear", 2018, "Self Development", "9780735211292", 320),
        new Book(5, "Deep Work", "Cal Newport", 2016, "Productivity", "9781455586691", 304),
        new Magazine(6, "National Geographic", "National Geographic Society", 2025, "Science", 202),
        new Magazine(7, "Tech Monthly", "Future Publishing", 2025, "Technology", 58),
        new Dvd(8, "The Social Network", "David Fincher", 2010, "Drama", 120),
        new Dvd(9, "Interstellar", "Christopher Nolan", 2014, "Sci-Fi", 169),
        new Dvd(10, "The Imitation Game", "Morten Tyldum", 2014, "Biography", 114)
    };

    public static List<Member> GetMembers() => new()
    {
        new Member(1, "Youssef Ahmed", "youssef@example.com"),
        new Member(2, "Mona Hassan", "mona@example.com"),
        new Member(3, "Omar Ali", "omar@example.com")
    };
}
