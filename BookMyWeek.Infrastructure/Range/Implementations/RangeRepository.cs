using BookMyWeek.Domain;
using BookMyWeek.Infrastructure.ConnectionFactory.Interfaces;
using BookMyWeek.Infrastructure.Range.Interfaces;
using BookMyWeek.Infrastructure.Range.Models;
using Dapper;

namespace BookMyWeek.Infrastructure.Range.Implementations;

public class RangeRepository : IRangeRepository
{
    private const string GetByUserQuery = "select * from public.range where userid = @userId";
    private const string AddQuery = "insert into public.range (rangeid, startrange, endrange, description, userid, allowview, agreed, eventid) VALUES (@RangeId, @StartRange, @EndRange, @Description, @UserId, @AllowView, @Agreed, @EventId)";
    private const string DeleteByRequestIdQuery = "DELETE FROM public.range WHERE eventId = @eventId";
    private const string AgreeAllByEventIdQuery = "UPDATE public.range SET agreed = true WHERE eventId = @eventId";
    private readonly IConnectionFactory _connectionFactory;
    
    public RangeRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<IEnumerable<EventRangeDatabase>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        return await connection.QueryAsync<EventRangeDatabase>(GetByUserQuery, new { userId });
    }

    public async Task Add(EventRangeDatabase eventRangeDatabase, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        await connection.ExecuteAsync(AddQuery, eventRangeDatabase);
    }

    public async Task DeleteByEventId(string eventId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        await connection.ExecuteAsync(DeleteByRequestIdQuery, new { eventId });
    }

    public async Task AgreeAllByEventId(string eventId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        await connection.ExecuteAsync(AgreeAllByEventIdQuery, new { eventId });
    }
}