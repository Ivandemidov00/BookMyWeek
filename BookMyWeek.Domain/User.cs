namespace BookMyWeek.Domain;

public record User(Guid UserId, string Name, string Description, byte[] Hash, byte[] Salt);