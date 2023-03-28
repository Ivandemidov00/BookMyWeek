using BookMyWeek.Application.Range.Models;
using BookMyWeek.Domain;

namespace BookMyWeek.Application.Range.Interfaces;

public interface IRangeService
{
    Task<IEnumerable<EventRange>> GetByUser(Guid userId, CancellationToken cancellationToken);

    Task Add(EventRangeDto eventRangeDto, CancellationToken cancellationToken);

    Task Agree(string eventId, CancellationToken cancellationToken);

    Task Cancel(string eventId, CancellationToken cancellationToken);

    Task<IEnumerable<AllowRange>> GetAllowedByUser(Guid userId, CancellationToken cancellationToken);

    Task AddAllowed(DateTimeRange dateTimeRange, CancellationToken cancellationToken);
}