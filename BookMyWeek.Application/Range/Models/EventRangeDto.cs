using BookMyWeek.Domain;

namespace BookMyWeek.Application.Range.Models;

public record EventRangeDto(Guid UserId, DateTime Start, DateTime End, string Description, bool AllowView) : DateTimeRange(Start, End)
{
    public bool Validate()
        => Start.Day == End.Day;
}