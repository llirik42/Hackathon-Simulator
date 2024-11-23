using HackathonProblem.HrDirector.services.storageService;
using Xunit;

namespace HackathonProblem.HrDirector.tests.db;

public class DbServiceTest : DbServiceTestsFixture
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void TestHackathonNotFound(int hackathonId)
    {
        var service = GetService<IStorageService>();
        var hackathon = service.FindHackathon(hackathonId);
        Assert.Null(hackathon);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void TestFindEmptyHackathon(int harmonization)
    {
        var service = GetService<IStorageService>();
        var hackathonId = service.CreateHackathon(harmonization);
        var hackathon = service.FindHackathon(hackathonId);
        Assert.NotNull(hackathon);
        Assert.Empty(hackathon.Teams);
        Assert.Equal(harmonization, Math.Round(hackathon.Harmonization));
    }
    
    [Fact]
    public void TestAverageHarmonization()
    {
        var service = GetService<IStorageService>();
        Assert.Throws<NoHackathonsFoundException>(() => service.GetAverageHarmonization());

        service.CreateHackathon(0);
        Assert.Equal(0, Math.Round(service.GetAverageHarmonization()));

        service.CreateHackathon(2);
        Assert.Equal(1, Math.Round(service.GetAverageHarmonization()));

        service.CreateHackathon(4);
        Assert.Equal(2, Math.Round(service.GetAverageHarmonization()));
    }
}
