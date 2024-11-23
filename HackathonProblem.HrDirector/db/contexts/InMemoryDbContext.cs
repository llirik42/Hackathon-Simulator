using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HackathonProblem.HrDirector.db.contexts;

public class InMemoryDbContext(DbSettings settings) : AbstractContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(settings.ConnectionString).UseSnakeCaseNamingConvention();
        optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }
}
