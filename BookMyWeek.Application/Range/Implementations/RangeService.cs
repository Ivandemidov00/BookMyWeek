using BookMyWeek.Application.Authentication.Interfaces;
using BookMyWeek.Application.Range.Interfaces;
using BookMyWeek.Domain;
using BookMyWeek.Infrastructure.AllowedRange.Interfaces;
using BookMyWeek.Infrastructure.Range.Interfaces;

namespace BookMyWeek.Application.Range.Implementations;

public class RangeService : IRangeService
{
    private readonly IRangeRepository _rangeRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IAllowedRangeRepository _allowedRangeRepository;
    public RangeService(IRangeRepository rangeRepository, IUserAccessor userAccessor, IAllowedRangeRepository allowedRangeRepository)
    {
        _rangeRepository = rangeRepository;
        _userAccessor = userAccessor;
        _allowedRangeRepository = allowedRangeRepository;
    }

    public async Task<IEnumerable<EventRange>> GetByUser(CancellationToken cancellationToken)
        => await _rangeRepository.GetByUser(_userAccessor.UserId, cancellationToken);

    public async Task<IEnumerable<AllowedEventRange>> GetAllowedByUser(CancellationToken cancellationToken)
        => await _allowedRangeRepository.GetByUser(_userAccessor.UserId, cancellationToken);
}