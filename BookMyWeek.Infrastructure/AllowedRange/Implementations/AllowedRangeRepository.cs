using BookMyWeek.Infrastructure.AllowedRange.Interfaces;
using BookMyWeek.Infrastructure.AllowedRange.Models;
using BookMyWeek.Infrastructure.ConnectionFactory.Interfaces;
using Dapper;

namespace BookMyWeek.Infrastructure.AllowedRange.Implementations;

public class AllowedRangeRepository : IAllowedRangeRepository
{
    private const string GetAllowedByUserQuery = "select * from public.allowedrange where userid = @userId";
    private const string AddQuery = "insert into public.allowedrange (allowedrangeid, startrange, endrange, userid) VALUES (@AllowedRangeId, @Start, @End, @UserId)";
    private const string DeleteQuery = "delete from public.allowedrange where allowedrangeid = @rangeId";

    private readonly IConnectionFactory _connectionFactory;

    public AllowedRangeRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<AllowedRangeDatabase>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        return await connection.QueryAsync<AllowedRangeDatabase>(GetAllowedByUserQuery, new { userId });
    }

    public async Task Add(AllowedRangeDatabase allowedRangeDatabase, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        await connection.ExecuteAsync(AddQuery, allowedRangeDatabase);
    }

    public async Task Delete(Guid rangeId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        await connection.ExecuteAsync(DeleteQuery, new { rangeId });
    }
}