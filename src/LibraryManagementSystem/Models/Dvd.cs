namespace LibraryManagementSystem.Models;

public class Dvd : LibraryItem
{
    public int DurationMinutes { get; set; }

    public Dvd(int id, string title, string director, int year, string category, int durationMinutes)
        : base(id, title, director, year, category)
    {
        DurationMinutes = durationMinutes;
    }

    public override string GetItemType() => "DVD";

    public override string GetDetails()
    {
        return base.GetDetails() + $" | Duration: {DurationMinutes} minutes";
    }
}
