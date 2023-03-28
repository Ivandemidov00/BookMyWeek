using BookMyWeek.Domain;

namespace BookMyWeek.Infrastructure.Range.Models;

public record EventRangeDatabase(Guid RangeId, DateTime StartRange, DateTime EndRange, string Description, Guid UserId, bool AllowView, bool Agreed, Guid EventId) : DateTimeRange(StartRange, EndRange);
