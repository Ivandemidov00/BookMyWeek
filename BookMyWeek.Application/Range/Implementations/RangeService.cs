using BookMyWeek.Application.Authentication.Interfaces;
using BookMyWeek.Application.Range.Interfaces;
using BookMyWeek.Application.Range.Models;
using BookMyWeek.Domain;
using BookMyWeek.Infrastructure.AllowedRange.Interfaces;
using BookMyWeek.Infrastructure.AllowedRange.Models;
using BookMyWeek.Infrastructure.Range.Interfaces;
using BookMyWeek.Infrastructure.Range.Models;
using Mapster;

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

    public async Task<IEnumerable<EventRange>> GetByUser(Guid userId, CancellationToken cancellationToken)
        => (await _rangeRepository.GetByUser(userId, cancellationToken)).Adapt<IEnumerable<EventRange>>();

    public async Task Add(EventRangeDto eventRangeDto, CancellationToken cancellationToken)
    {
        if (!eventRangeDto.Validate())
            throw new Exception();
        if (!await ValidateNewEventRange(eventRangeDto, cancellationToken))
            throw new Exception();
        var eventId = Guid.NewGuid();
        await _rangeRepository.Add(new EventRangeDatabase(Guid.NewGuid(), eventRangeDto.Start, eventRangeDto.End, eventRangeDto.Description, _userAccessor.UserId, eventRangeDto.AllowView, true, eventId), cancellationToken);
        await _rangeRepository.Add(new EventRangeDatabase(Guid.NewGuid(), eventRangeDto.Start, eventRangeDto.End, eventRangeDto.Description, eventRangeDto.UserId, eventRangeDto.AllowView, false, eventId), cancellationToken);
    }

    public async Task Agree(string eventId, CancellationToken cancellationToken)
        => await _rangeRepository.AgreeAllByEventId(eventId, cancellationToken);

    public async Task Cancel(string eventId, CancellationToken cancellationToken)
        => await _rangeRepository.DeleteByEventId(eventId, cancellationToken);

    public async Task<IEnumerable<AllowRange>> GetAllowedByUser(Guid userId,
        CancellationToken cancellationToken)
        => (await _allowedRangeRepository.GetByUser(userId, cancellationToken)).Adapt<IEnumerable<AllowRange>>();

    public async Task AddAllowed(DateTimeRange dateTimeRange, CancellationToken cancellationToken)
    {
        var allowedRangesForCurrentUser = await _allowedRangeRepository.GetByUser(_userAccessor.UserId, cancellationToken);
        var currentAllowedRange = allowedRangesForCurrentUser.FirstOrDefault(range => range.StartRange.DayOfYear == dateTimeRange.Start.DayOfYear);
        if (currentAllowedRange != null)
            await _allowedRangeRepository.Delete(currentAllowedRange.AllowedRangeId, cancellationToken);
        await _allowedRangeRepository.Add(
            new AllowedRangeDatabase(Guid.NewGuid(), dateTimeRange.Start, dateTimeRange.End, _userAccessor.UserId),
            cancellationToken);
    }

    private async Task<bool> ValidateNewEventRange(EventRangeDto eventRangeDto, CancellationToken cancellationToken)
    {
        var invitingUserAllowedEventRange = (await _allowedRangeRepository.GetByUser(_userAccessor.UserId, cancellationToken)).Where(allowRange => allowRange.EndRange.Day == eventRangeDto.End.Day).ToList();
        if (!invitingUserAllowedEventRange.Any())
        {
            return false;
        }
        
        var calledUserAllowedEventRange = (await _allowedRangeRepository.GetByUser(eventRangeDto.UserId, cancellationToken)).Where(allowRange => allowRange.EndRange.Day == eventRangeDto.End.Day).ToList();
        if (!calledUserAllowedEventRange.Any())
        {
            return invitingUserAllowedEventRange.Select(allowRange => allowRange.Includes(eventRangeDto)).Any();
        }
        
        return invitingUserAllowedEventRange.Select(allowRange => allowRange.Includes(eventRangeDto)).Any() && calledUserAllowedEventRange.Select(allowRange => allowRange.Includes(eventRangeDto)).Any();
    }
}