namespace Core.Entities;

public class InvoiceEntry : EntityBase
{
    // EntityFrameworkCore related empty default constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public InvoiceEntry()
    {
    }

    public string Name { get; set; }

    public long TrackedHours { get; set; }
}
