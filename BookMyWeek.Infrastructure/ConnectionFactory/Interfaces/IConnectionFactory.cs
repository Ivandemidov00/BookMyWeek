using System.Data.Common;

namespace BookMyWeek.Infrastructure.ConnectionFactory.Interfaces;

public interface IConnectionFactory
{
    Task<DbConnection> CreateAsync(CancellationToken cancellationToken);
}