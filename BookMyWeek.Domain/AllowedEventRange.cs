namespace BookMyWeek.Domain;

public record AllowedEventRange(DateTime Start, DateTime End) : DateTimeRange(Start, End);