using BookMyWeek.Domain;

namespace BookMyWeek.Application.Range.Interfaces;

public interface IRangeService
{
    Task<IEnumerable<EventRange>> GetByUser(CancellationToken cancellationToken);
    Task<IEnumerable<AllowedEventRange>> GetAllowedByUser(CancellationToken cancellationToken);
}