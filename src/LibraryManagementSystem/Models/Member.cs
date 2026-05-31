namespace LibraryManagementSystem.Models;

public class Member
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.Today;
    public List<int> BorrowedItemIds { get; set; } = new();

    public Member(int id, string fullName, string email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }

    public string GetDetails()
    {
        return $"[{Id}] {FullName} | {Email} | Joined: {JoinedAt:yyyy-MM-dd} | Borrowed Items: {BorrowedItemIds.Count}";
    }
}
