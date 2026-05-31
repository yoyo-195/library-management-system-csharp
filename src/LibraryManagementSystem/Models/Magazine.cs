namespace LibraryManagementSystem.Models;

public class Magazine : LibraryItem
{
    public int IssueNumber { get; set; }

    public Magazine(int id, string title, string publisher, int year, string category, int issueNumber)
        : base(id, title, publisher, year, category)
    {
        IssueNumber = issueNumber;
    }

    public override string GetItemType() => "Magazine";

    public override string GetDetails()
    {
        return base.GetDetails() + $" | Issue: {IssueNumber}";
    }
}
