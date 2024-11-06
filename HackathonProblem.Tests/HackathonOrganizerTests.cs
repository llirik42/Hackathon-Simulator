using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using Moq;
using Xunit;

namespace HackathonProblem.Tests;

public class HackathonOrganizerTests : GlobalFixture
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

        var hackathon = GetService<IHackathonOrganizer>()
            .Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(count, Math.Round(hackathon.Harmonization));
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

        var hackathon = GetService<IHackathonOrganizer>()
            .Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(2.526, Math.Round(hackathon.Harmonization, 3));
    }

    [Fact]
    public void TestHrManagerCallsCount()
    {
        var teamLeads = new List<Employee>();
        var juniors = new List<Employee>();
        var teamLeadsWishlists = new List<Wishlist>();
        var juniorsWishlists = new List<Wishlist>();

        var mockedCalculator = new Mock<IHarmonizationCalculator>();
        var mockedStrategy = new Mock<ITeamBuildingStrategy>();

        mockedStrategy.Setup(x => x.BuildTeams(
            It.IsAny<List<Employee>>(),
            It.IsAny<List<Employee>>(),
            It.IsAny<List<Wishlist>>(),
            It.IsAny<List<Wishlist>>()
        )).Returns(new List<Team>());

        var organizer = new HackathonOrganizer.HackathonOrganizer(mockedCalculator.Object, mockedStrategy.Object);
        organizer.Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        mockedStrategy.Verify(x => x.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists), Times.Once);
    }
}
