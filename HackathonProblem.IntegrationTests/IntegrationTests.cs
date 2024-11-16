using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.tests;
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
}
