using Contracts;
using Xunit;

namespace Tests;

public class HackathonOrganizerTests : Fixture
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
        var teamLeads = getSimpleEmployees(count);
        var juniors = getSimpleEmployees(count);
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

        var members = GetService<IHackathonOrganizer>()
            .Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(count, Math.Round(members.Harmonization));
    }

    [Fact]
    public void TestSomePreferences()
    {
        const int count = 4;
        var teamLeads = getSimpleEmployees(count);
        var juniors = getSimpleEmployees(count);

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

        var members = GetService<IHackathonOrganizer>()
            .Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

        Assert.Equal(2.526, Math.Round(members.Harmonization, 3));
    }

    private static void ShiftLeft<T>(List<T> list, int shift)
    {
        var copy = new List<T>(list);

        for (var i = shift; i < list.Count; i++) list[i - shift] = list[i];

        for (var i = list.Count - shift; i < list.Count; i++) list[i] = copy[i + shift - list.Count];
    }
}
