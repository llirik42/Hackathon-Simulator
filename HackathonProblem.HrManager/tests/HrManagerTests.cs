using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.tests;
using Moq;
using Xunit;

namespace HackathonProblem.HrManager.tests;

public class HrManagerTests : HrManagerTestsFixture
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
        var teamLeads = TestUtils.GetSimpleEmployees(count);
        var juniors = TestUtils.GetSimpleEmployees(count);

        var wishlistProvider = GetService<IWishlistProvider>();
        var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniors, teamLeads);
        var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniors, teamLeads);

        var mockedHrDirector = new Mock<IHrDirector>();
        var hrManager = new domain.HrManager(mockedHrDirector.Object);
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
        var teamLeads = TestUtils.GetSimpleEmployees(count);
        var juniors = TestUtils.GetSimpleEmployees(count);
        var teamLeadsWishlists = new List<Wishlist>();
        var juniorsWishlists = new List<Wishlist>();

        var desiredEmployees = Enumerable.Range(1, count).ToList();

        for (var i = 0; i < count; i++)
        {
            var id = i + 1;
            teamLeadsWishlists.Add(new Wishlist(id, desiredEmployees.ToArray()));
            juniorsWishlists.Add(new Wishlist(id, desiredEmployees.ToArray()));
            TestUtils.ShiftLeft(desiredEmployees, 1);
        }

        var expectedTeams = new List<Team>();
        for (var i = 0; i < count; i++) expectedTeams.Add(new Team(teamLeads[i], juniors[i]));

        var hrManager = GetService<IHrManager>();
        
        var actualTeams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(expectedTeams, actualTeams);
    }
}
