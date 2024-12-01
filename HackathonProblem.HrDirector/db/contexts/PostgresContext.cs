using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.HrDirector.db.contexts;

public class PostgresContext : AbstractContext
{
    private readonly DbConfig _config;

    public PostgresContext(DbConfig config)
    {
        _config = config;
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config.ConnectionString).UseSnakeCaseNamingConvention();
    }
}
