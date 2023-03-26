using BookMyWeek.Domain;
using BookMyWeek.Infrastructure.ConnectionFactory.Interfaces;
using BookMyWeek.Infrastructure.Range.Interfaces;
using Dapper;

namespace BookMyWeek.Infrastructure.Range.Implementations;

public class RangeRepository : IRangeRepository
{
    private const string GetByUserQuery = "select * from public.range where userid = @userId";
    private readonly IConnectionFactory _connectionFactory;
    
    public RangeRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<IEnumerable<EventRange>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        return await connection.QueryAsync<EventRange>(GetByUserQuery, new { userId });
    }
}