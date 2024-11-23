using HackathonProblem.HrDirector.db;
using HackathonProblem.HrDirector.db.contexts;
using HackathonProblem.HrDirector.services.storageService;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.HrDirector.tests.db;

public class DbServiceTestsFixture : IDisposable
{
    private readonly Action _deleteDbAction;
    private readonly IServiceScope _scope;

    protected DbServiceTestsFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<DbSettings>(_ => new DbSettings { ConnectionString = "Data Source=:memory:" });
        serviceCollection.AddDbContextFactory<InMemoryDbContext>();
        serviceCollection.AddScoped<IStorageService, DbStorageService<InMemoryDbContext>>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _scope = serviceProvider.CreateScope();

        _deleteDbAction = () => _scope.ServiceProvider.GetRequiredService<IDbContextFactory<InMemoryDbContext>>()
            .CreateDbContext().Database
            .EnsureDeleted();
    }

    public virtual void Dispose()
    {
        _deleteDbAction();
        GC.SuppressFinalize(this);
    }

    protected T GetService<T>() where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }
}
