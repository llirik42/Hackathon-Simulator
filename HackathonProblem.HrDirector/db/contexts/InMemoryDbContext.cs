using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HackathonProblem.HrDirector.db.contexts;

public class InMemoryDbContext(DbConfig config) : AbstractContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(config.ConnectionString).UseSnakeCaseNamingConvention();
        optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }
}
