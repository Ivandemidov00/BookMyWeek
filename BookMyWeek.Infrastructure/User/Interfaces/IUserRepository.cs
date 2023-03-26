namespace BookMyWeek.Infrastructure.User.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Domain.UserDatabase>> GetAll(CancellationToken cancellationToken);

    Task<Domain.UserDatabase> GetById(Guid userId, CancellationToken cancellationToken);

    Task<Domain.UserDatabase> GetByUsername(string username, CancellationToken cancellationToken);

    Task Add(Domain.UserDatabase userDatabase, CancellationToken cancellationToken);
}