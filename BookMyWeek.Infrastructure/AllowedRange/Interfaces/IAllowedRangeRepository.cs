using BookMyWeek.Domain;

namespace BookMyWeek.Infrastructure.AllowedRange.Interfaces;

public interface IAllowedRangeRepository
{
    Task<IEnumerable<AllowedEventRange>> GetByUser(Guid userId, CancellationToken cancellationToken);
}