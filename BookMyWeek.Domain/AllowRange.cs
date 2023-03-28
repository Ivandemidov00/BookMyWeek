namespace BookMyWeek.Domain;

public record AllowRange(DateTime Start, DateTime End) : DateTimeRange(Start, End);