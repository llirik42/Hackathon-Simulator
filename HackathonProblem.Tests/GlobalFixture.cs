using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db;
using HackathonProblem.Db.contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HackathonProblem.Tests;

public class GlobalFixture : IDisposable
{
    private readonly Action _deleteDbAction;
    private readonly IServiceScope _scope;

    protected GlobalFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IWishlistProvider, RandomWishlistsProvider.RandomWishlistsProvider>();
        serviceCollection.AddTransient<IHarmonizationCalculator, HrDirector.HrDirector>();
        serviceCollection.AddTransient<ITeamBuildingStrategy, HrManager.HrManager>();
        serviceCollection.AddTransient<IHackathonOrganizer, HackathonOrganizer.HackathonOrganizer>();

        serviceCollection.AddTransient<DbSettings>(_ => new DbSettings { ConnectionString = "Data Source=:memory:" });
        serviceCollection.AddDbContextFactory<InMemoryDbContext>();
        serviceCollection.AddScoped<IDbService, DbService<InMemoryDbContext>>();

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

    private static Employee GetSimpleEmployee(int id)
    {
        return new Employee(id, $"{id}");
    }

    protected static List<Employee> GetSimpleEmployees(int count)
    {
        var employees = new List<Employee>();

        for (var i = 0; i < count; i++)
        {
            var id = i + 1;
            employees.Add(GetSimpleEmployee(id));
        }

        return employees;
    }

    protected static void ShiftLeft<T>(List<T> list, int shift)
    {
        var copy = new List<T>(list);

        for (var i = shift; i < list.Count; i++) list[i - shift] = list[i];

        for (var i = list.Count - shift; i < list.Count; i++) list[i] = copy[i + shift - list.Count];
    }
}
