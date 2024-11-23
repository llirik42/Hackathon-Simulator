using HackathonProblem.Common.domain.contracts;

namespace HackathonProblem.HrDirector.tests.hrDirector;

public class HrDirectorTestsFixture
{
    private readonly IServiceScope _scope;

    protected HrDirectorTestsFixture()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<IHrDirector, domain.HrDirector>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _scope = serviceProvider.CreateScope();
    }

    protected T GetService<T>() where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }
}
