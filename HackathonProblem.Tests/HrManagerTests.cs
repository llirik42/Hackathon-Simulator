using HackathonProblem.Contracts;
using Moq;
using Xunit;

namespace HackathonProblem.Tests;

public class HrManagerTests : GlobalFixture
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public void TestTeamsCount(int count)
    {
        var teamLeads = GetSimpleEmployees(count);
        var juniors = GetSimpleEmployees(count);

        var wishlistProvider = GetService<IWishlistProvider>();
        var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniors, teamLeads);
        var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniors, teamLeads);

        var mockedCalculator = new Mock<IHarmonizationCalculator>();
        var hrManager = new HrManager.HrManager(mockedCalculator.Object);
        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(count, teams.Count());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public void TestPerfectMatch(int count)
    {
        var teamLeads = GetSimpleEmployees(count);
        var juniors = GetSimpleEmployees(count);
        var teamLeadsWishlists = new List<Wishlist>();
        var juniorsWishlists = new List<Wishlist>();

        var desiredEmployees = Enumerable.Range(1, count).ToList();

        for (var i = 0; i < count; i++)
        {
            var id = i + 1;
            teamLeadsWishlists.Add(new Wishlist(id, desiredEmployees.ToArray()));
            juniorsWishlists.Add(new Wishlist(id, desiredEmployees.ToArray()));
            ShiftLeft(desiredEmployees, 1);
        }

        var expectedTeams = new List<Team>();
        for (var i = 0; i < count; i++) expectedTeams.Add(new Team(teamLeads[i], juniors[i]));

        var mockedCalculator = new Mock<IHarmonizationCalculator>();
        var hrManager = new HrManager.HrManager(mockedCalculator.Object);
        var actualTeams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(expectedTeams, actualTeams);
    }

    [Fact]
    public void TestSomePreferences()
    {
        const int count = 4;
        var teamLeads = GetSimpleEmployees(count);
        var juniors = GetSimpleEmployees(count);

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

        var actualTeams = GetService<ITeamBuildingStrategy>()
            .BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(expectedTeams, actualTeams);
    }
}
