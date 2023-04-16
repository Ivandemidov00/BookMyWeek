using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookMyWeek.Infrastructure.DataProvider.EntityFramework;

public class ApplicationContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PgSql") ?? string.Empty);
}