using BookMyWeek.Domain;

namespace BookMyWeek.Infrastructure.AllowedRange.Models;

public record AllowedRangeDatabase(Guid AllowedRangeId, DateTime StartRange, DateTime EndRange, Guid UserId) : DateTimeRange(StartRange, EndRange);