using BookMyWeek.Application.Range.Interfaces;
using BookMyWeek.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookMyWeek.Application.Controllers;

[ApiController]
[Route("api/range")]
public class RangeController
{
    private readonly IRangeService _rangeService;

    public RangeController(IRangeService rangeService)
    {
        _rangeService = rangeService;
    }

    [HttpGet]
    public async Task<IEnumerable<EventRange>> GetByUser([FromRoute(Name = "userDatabase-id")] string userId, CancellationToken cancellationToken)
        => await _rangeService.GetByUser(cancellationToken);
    
    [HttpGet("allowed")]
    public async Task<IEnumerable<AllowedEventRange>> GetAllowedByUser([FromRoute(Name = "userDatabase-id")] string userId, CancellationToken cancellationToken)
        => await _rangeService.GetAllowedByUser(cancellationToken);
}