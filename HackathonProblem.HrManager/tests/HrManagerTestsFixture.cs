using HackathonProblem.Common.domain.contracts;
using Moq;

namespace HackathonProblem.HrManager.tests;

public class HrManagerTestsFixture
{
    private readonly IServiceScope _scope;

    protected HrManagerTestsFixture()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<IWishlistProvider, RandomWishlistsProvider.RandomWishlistsProvider>();
        serviceCollection.AddTransient<IHrDirector>(_ => new Mock<IHrDirector>().Object);
        serviceCollection.AddTransient<IHrManager, domain.HrManager>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _scope = serviceProvider.CreateScope();
    }

    protected T GetService<T>() where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }
}
