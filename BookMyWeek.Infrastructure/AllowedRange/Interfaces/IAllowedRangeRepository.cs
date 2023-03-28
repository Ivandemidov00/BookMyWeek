using BookMyWeek.Infrastructure.AllowedRange.Models;

namespace BookMyWeek.Infrastructure.AllowedRange.Interfaces;

public interface IAllowedRangeRepository
{
    Task<IEnumerable<AllowedRangeDatabase>> GetByUser(Guid userId, CancellationToken cancellationToken);

    Task Add(AllowedRangeDatabase allowedRangeDatabase, CancellationToken cancellationToken);

    Task Delete(Guid rangeId, CancellationToken cancellationToken);
}