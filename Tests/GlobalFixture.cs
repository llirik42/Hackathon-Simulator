using Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class GlobalFixture
{
    private readonly IServiceScope _scope;

    protected GlobalFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IWishlistProvider, RandomWishlistsProvider.RandomWishlistsProvider>();
        serviceCollection.AddTransient<IHarmonizationCalculator, HrDirector.HrDirector>();
        serviceCollection.AddTransient<ITeamBuildingStrategy, HrManager.HrManager>();
        serviceCollection.AddTransient<IHackathonOrganizer, HackathonOrganizer.HackathonOrganizer>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _scope = serviceProvider.CreateScope();
    }

    protected T GetService<T>() where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }

    protected static List<Employee> GetSimpleEmployees(int count)
    {
        var employees = new List<Employee>();

        for (var i = 0; i < count; i++)
        {
            var id = i + 1;
            employees.Add(new Employee(id, $"{id}"));
        }

        return employees;
    }
}
