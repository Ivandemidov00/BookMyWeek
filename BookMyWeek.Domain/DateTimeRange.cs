namespace BookMyWeek.Domain;

public record DateTimeRange(DateTime Start, DateTime End) 
{
    public bool Includes(DateTime value)
        => Start <= value && value <= End;

    public bool Includes(DateTimeRange range)
        => Start <= range.Start && range.End <= End;
}