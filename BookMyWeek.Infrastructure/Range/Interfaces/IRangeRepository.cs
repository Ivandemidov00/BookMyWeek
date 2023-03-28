using BookMyWeek.Infrastructure.Range.Models;

namespace BookMyWeek.Infrastructure.Range.Interfaces;

public interface IRangeRepository
{
    Task<IEnumerable<EventRangeDatabase>> GetByUser(Guid userId, CancellationToken cancellationToken);

    Task Add(EventRangeDatabase eventRangeDatabase, CancellationToken cancellationToken);

    Task DeleteByEventId(string requestId, CancellationToken cancellationToken);

    Task AgreeAllByEventId(string eventId, CancellationToken cancellationToken);
}