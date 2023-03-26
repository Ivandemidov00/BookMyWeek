namespace BookMyWeek.Domain;

public record DateTimeRange(DateTime Start, DateTime End)
{
    bool Includes(DateTime value)
        => Start <= value && value <= End;

    bool Includes(DateTimeRange range)
        => Start <= range.Start && range.End <= End;
}