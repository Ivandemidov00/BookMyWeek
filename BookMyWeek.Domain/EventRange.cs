namespace BookMyWeek.Domain;

public record EventRange(DateTime Start, DateTime End, string Description, bool AllowView, bool Agreed) : DateTimeRange(Start, End);
