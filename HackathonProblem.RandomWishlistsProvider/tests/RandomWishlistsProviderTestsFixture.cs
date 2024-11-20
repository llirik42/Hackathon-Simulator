using HackathonProblem.Common.domain.contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HackathonProblem.RandomWishlistsProvider.tests;

public class RandomWishlistsProviderTestsFixture
{
    private readonly IServiceScope _scope;

    protected RandomWishlistsProviderTestsFixture()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddTransient<IWishlistProvider, RandomWishlistsProvider>();
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _scope = serviceProvider.CreateScope();
    }

    protected T GetService<T>() where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }
}
