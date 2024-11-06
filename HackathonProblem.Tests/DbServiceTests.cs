using HackathonProblem.Contracts.services;
using HackathonProblem.Db.exceptions;
using Moq;
using Xunit;

namespace HackathonProblem.Tests;

public class DbServiceTest : GlobalFixture
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void TestHackathonNotFound(int hackathonId)
    {
        var service = GetService<IDbService>();
        var hackathon = service.FindHackathon(hackathonId);
        Assert.Null(hackathon);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void TestFindEmptyHackathon(int harmonization)
    {
        var service = GetService<IDbService>();
        var hackathonId = service.CreateHackathon(harmonization);
        var hackathon = service.FindHackathon(hackathonId);
        Assert.NotNull(hackathon);
        Assert.Empty(hackathon.Teams);
        Assert.Equal(harmonization, Math.Round(hackathon.Harmonization));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    public void TestFindHackathon(int count)
    {
        const int harmonization = 1;
        var service = GetService<IDbService>();
        var hackathonId = service.CreateHackathon(harmonization);

        var juniors = GetSimpleEmployees(count);
        var teamLeads = GetSimpleEmployees(count);

        var wishlistProvider = GetService<IWishlistProvider>();
        var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniors, teamLeads);
        var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniors, teamLeads);

        var mockedCalculator = new Mock<IHarmonizationCalculator>();
        var hrManager = new HrManager.HrManager(mockedCalculator.Object);
        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists).ToList();

        for (var i = 0; i < count; i++)
        {
            service.CreateJunior(juniors[i]);
            service.CreateTeamLead(teamLeads[i]);
        }

        service.AddTeams(hackathonId, teams);

        var hackathon = service.FindHackathon(hackathonId);
        Assert.NotNull(hackathon);
        Assert.Equal(harmonization, Math.Round(hackathon.Harmonization));
        Assert.Equal(teams, hackathon.Teams);
    }

    [Fact]
    public void TestAverageHarmonization()
    {
        var service = GetService<IDbService>();
        Assert.Throws<NoHackathonsFoundException>(() => service.GetAverageHarmonization());

        service.CreateHackathon(0);
        Assert.Equal(0, Math.Round(service.GetAverageHarmonization()));

        service.CreateHackathon(2);
        Assert.Equal(1, Math.Round(service.GetAverageHarmonization()));

        service.CreateHackathon(4);
        Assert.Equal(2, Math.Round(service.GetAverageHarmonization()));
    }
}
