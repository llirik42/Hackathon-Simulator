using HackathonProblem.Common.domain.contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HackathonProblem.IntegrationTests;

public class IntegrationTestsFixture
{
    private readonly IServiceScope _scope;

    protected IntegrationTestsFixture()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddTransient<IHrDirector, HrDirector.domain.HrDirector>();
        serviceCollection.AddTransient<IHrManager, HrManager.domain.HrManager>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _scope = serviceProvider.CreateScope();
    }

    protected T GetService<T>() where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }
}
