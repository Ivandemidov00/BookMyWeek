using BookMyWeek.Infrastructure.ConnectionFactory.Interfaces;
using BookMyWeek.Infrastructure.User.Interfaces;
using Dapper;

namespace BookMyWeek.Infrastructure.User.Implementations;

public class UserRepository : IUserRepository
{
    private const string GetAllQuery = "select * from public.user";
    private const string GetByIdQuery = "select * from public.user where userid = @userId";
    private const string GetByUsernameQuery = "select * from public.user where name = @username";
    private const string AddQuery = @"insert into public.user (userid, name, description, hash, salt) VALUES (@UserId, @Name, @Description, @Hash, @Salt)";
    private readonly IConnectionFactory _connectionFactory;
    
    public UserRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Domain.UserDatabase>> GetAll(CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        return await connection.QueryAsync<Domain.UserDatabase>(GetAllQuery, cancellationToken);
    }

    public async Task<Domain.UserDatabase> GetById(Guid userId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        return await connection.QueryFirstOrDefaultAsync<Domain.UserDatabase>(GetByIdQuery, userId);
    }

    public async Task<Domain.UserDatabase> GetByUsername(string username, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        return await connection.QueryFirstOrDefaultAsync<Domain.UserDatabase>(GetByUsernameQuery, new { username });
    }

    public async Task Add(Domain.UserDatabase userDatabase, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.CreateAsync(cancellationToken);
        await connection.ExecuteAsync(AddQuery, userDatabase);
    }
}