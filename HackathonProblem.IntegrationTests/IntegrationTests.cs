using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.tests;
using HackathonProblem.HrDirector.services.storageService;
using Moq;
using Xunit;

namespace HackathonProblem.IntegrationTests;

public class IntegrationTests : IntegrationTestsFixture
{
    [Fact]
    public void TestSomePreferences()
    {
        const int count = 4;
        var teamLeads = TestUtils.GetSimpleEmployees(count);
        var juniors = TestUtils.GetSimpleEmployees(count);

        List<Wishlist> teamLeadsWishlists =
        [
            new(1, [1, 2, 3, 4]),
            new(2, [1, 2, 3, 4]),
            new(3, [1, 2, 3, 4]),
            new(4, [1, 2, 3, 4])
        ];

        List<Wishlist> juniorsWishlists =
        [
            new(1, [2, 4, 3, 1]),
            new(2, [2, 4, 1, 3]),
            new(3, [1, 2, 3, 4]),
            new(4, [3, 4, 1, 2])
        ];

        List<Team> expectedTeams =
        [
            new(teamLeads[1], juniors[0]),
            new(teamLeads[3], juniors[1]),
            new(teamLeads[0], juniors[2]),
            new(teamLeads[2], juniors[3])
        ];

        var actualTeams = GetService<IHrManager>()
            .BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(expectedTeams, actualTeams);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    public void TestFindHackathon(int count)
    {
        const int harmonization = 1;
        var service = GetService<IStorageService>();
        var hackathonId = service.CreateHackathon(harmonization);

        var juniors = TestUtils.GetSimpleEmployees(count);
        var teamLeads = TestUtils.GetSimpleEmployees(count);
        var juniorsIds = juniors.Select(j => j.Id).ToList();
        var teamLeadsIds = teamLeads.Select(team => team.Id).ToList();
        
        var wishlistProvider = GetService<IWishlistProvider>();
        var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniorsIds, teamLeadsIds);
        var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniorsIds, teamLeadsIds);

        var mockedCalculator = new Mock<IHrDirector>();
        var hrManager = new HrManager.domain.HrManager(mockedCalculator.Object);
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
}
