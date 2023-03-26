using System.Data.Common;
using BookMyWeek.Infrastructure.ConnectionFactory.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BookMyWeek.Infrastructure.ConnectionFactory.Implementations;

public class ConnectionFactory : IConnectionFactory
{
    private const string PgSqlConnectionString = "PgSql";
    private readonly IConfiguration _configuration;
    
    public ConnectionFactory(IConfiguration configuration)
        => _configuration = configuration;

    public async Task<DbConnection> CreateAsync(CancellationToken cancellationToken)
    {
        try
        {
            var connection =
                new NpgsqlConnection(_configuration.GetConnectionString(PgSqlConnectionString));
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
        catch (Exception exception)
        {
            throw new Exception("PgSql -> Create", exception);
        }
    }
}