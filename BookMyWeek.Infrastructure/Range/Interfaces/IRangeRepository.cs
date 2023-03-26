using BookMyWeek.Domain;

namespace BookMyWeek.Infrastructure.Range.Interfaces;

public interface IRangeRepository
{
    Task<IEnumerable<EventRange>> GetByUser(Guid userId, CancellationToken cancellationToken);
}