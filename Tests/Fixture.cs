using Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class Fixture
{
    private readonly IServiceScope _scope;

    protected Fixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IHarmonizationCalculator, HrDirector.HrDirector>();
        serviceCollection.AddTransient<ITeamBuildingStrategy, HrManager.HrManager>();
        serviceCollection.AddTransient<IHackathonOrganizer, HackathonOrganizer.HackathonOrganizer>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _scope = serviceProvider.CreateScope();
    }

    protected T GetService<T>()
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }
}
