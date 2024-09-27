using HackathonProblem.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HackathonProblem.Tests;

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
