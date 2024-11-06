using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.Db.contexts;

public class PostgresContext : AbstractContext
{
    private readonly DbSettings _settings;
    
    public PostgresContext(DbSettings settings)
    {
        _settings = settings;
        Database.Migrate();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_settings.ConnectionString).UseSnakeCaseNamingConvention();
    }
}
