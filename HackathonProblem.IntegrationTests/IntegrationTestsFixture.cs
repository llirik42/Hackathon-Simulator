using HackathonProblem.Common.domain.contracts;
using HackathonProblem.HrDirector.db;
using HackathonProblem.HrDirector.db.contexts;
using HackathonProblem.HrDirector.services.storageService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HackathonProblem.IntegrationTests;

public class IntegrationTestsFixture : IDisposable
{
    private readonly Action _deleteDbAction;
    private readonly IServiceScope _scope;

    protected IntegrationTestsFixture()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<DbSettings>(_ => new DbSettings { ConnectionString = "Data Source=:memory:" });
        serviceCollection.AddSingleton<IWishlistProvider, RandomWishlistsProvider.RandomWishlistsProvider>();
        serviceCollection.AddTransient<IHrDirector, HrDirector.domain.HrDirector>();
        serviceCollection.AddTransient<IHrManager, HrManager.domain.HrManager>();
        serviceCollection.AddScoped<IStorageService, DbStorageService<InMemoryDbContext>>();
        serviceCollection.AddDbContextFactory<InMemoryDbContext>();

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
