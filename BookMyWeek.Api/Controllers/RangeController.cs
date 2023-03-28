using BookMyWeek.Application.Range.Interfaces;
using BookMyWeek.Application.Range.Models;
using BookMyWeek.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookMyWeek.Application.Controllers;

[ApiController]
[Route("api/range")]
public class RangeController : ControllerBase
{
    private readonly IRangeService _rangeService;

    public RangeController(IRangeService rangeService)
    {
        _rangeService = rangeService;
    }

    [HttpGet]
    public async Task<IEnumerable<EventRange>> GetByUser([FromQuery(Name = "userId")] string userId, CancellationToken cancellationToken)
        => await _rangeService.GetByUser(Guid.Parse(userId), cancellationToken);

    [HttpPost]
    public async Task AddRange([FromBody] EventRangeDto eventRangeDto, CancellationToken cancellationToken)
        => await _rangeService.Add(eventRangeDto, cancellationToken);

    [HttpPost("select")]
    public async Task AgreeRange([FromQuery(Name = "eventId")] string eventId, CancellationToken cancellationToken)
        => await _rangeService.Agree(eventId, cancellationToken);

    [HttpDelete("select")]
    public async Task CancelRange([FromQuery(Name = "eventId")] string eventId, CancellationToken cancellationToken)
        => await _rangeService.Cancel(eventId, cancellationToken);
            
    [HttpGet("allowed")]
    public async Task<IEnumerable<AllowRange>> GetAllowedByUser([FromQuery(Name = "userId")] string userId, CancellationToken cancellationToken)
        => await _rangeService.GetAllowedByUser(Guid.Parse(userId), cancellationToken);

    [HttpPost("allowed")]
    public async Task AddAllowedRange([FromBody] DateTimeRange dateTimeRange, CancellationToken cancellationToken)
        => await _rangeService.AddAllowed(dateTimeRange, cancellationToken);

}