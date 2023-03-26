namespace BookMyWeek.Domain;

public record Day(IEnumerable<EventRange> Description);