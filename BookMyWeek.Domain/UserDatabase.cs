namespace BookMyWeek.Domain;

public record UserDatabase(Guid UserId, string Name, string Description, byte[] Hash, byte[] Salt);