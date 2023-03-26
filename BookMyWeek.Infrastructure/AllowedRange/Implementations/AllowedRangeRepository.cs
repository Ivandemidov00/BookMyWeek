using BookMyWeek.Domain;
using BookMyWeek.Infrastructure.AllowedRange.Interfaces;
using BookMyWeek.Infrastructure.ConnectionFactory.Interfaces;
using Dapper;

namespace BookMyWeek.Infrastructure.AllowedRange.Implementations;

public class AllowedRangeRepository : IAllowedRangeRepository
{
    private const string GetAllowedByUserQuery = "select * from public.range where userid = @userId";
    private readonly IConnectionFactory _connectionFactory;

    public AllowedRangeRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<AllowedEventRange>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        return await connection.QueryAsync<AllowedEventRange>(GetAllowedByUserQuery, new { userId });
    }
}