using System.Collections;
using HackathonProblem.Contracts;
using Xunit;

namespace HackathonProblem.Tests;

public class EmployeesGenerator : IEnumerable<object[]>
{
    private static readonly List<Employee> TeamLeads1 = [new(1, "Team-lead 1")];
    private static readonly List<Employee> Juniors1 = [new(1, "Junior 1")];
    private static readonly List<Employee>[] Members1 = [TeamLeads1, Juniors1];

    private static readonly List<Employee> TeamLeads2 =
    [
        new(1, "Team-lead 1"),
        new(2, "Team-lead 2")
    ];

    private static readonly List<Employee> Juniors2 =
    [
        new(13, "Junior 13")
    ];

    private static readonly List<Employee>[] Members2 = [TeamLeads2, Juniors2];

    private static readonly List<Employee> TeamLeads3 =
    [
        new(1, "Team-lead 1"),
        new(2, "Team-lead 2")
    ];

    private static readonly List<Employee> Juniors3 =
    [
        new(6, "Junior 6"),
        new(17, "Junior 17")
    ];

    private static readonly List<Employee>[] Members3 = [TeamLeads3, Juniors3];

    private static readonly List<Employee> TeamLeads4 =
    [
        new(1, "Team-lead 1"),
        new(2, "Team-lead 2"),
        new(3, "Team-lead 3"),
        new(4, "Team-lead 4")
    ];

    private static readonly List<Employee> Juniors4 =
    [
        new(101, "Junior 101"),
        new(102, "Junior 102"),
        new(103, "Junior 103"),
        new(104, "Junior 104"),
        new(105, "Junior 105"),
        new(106, "Junior 106"),
        new(107, "Junior 107")
    ];

    private static readonly List<Employee>[] Members4 = [TeamLeads4, Juniors4];

    private static readonly List<List<Employee>[]> Data =
    [
        Members1,
        Members2,
        Members3,
        Members4
    ];

    public IEnumerator<object[]> GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class WishlistsProviderTests : GlobalFixture
{
    [Theory]
    [ClassData(typeof(EmployeesGenerator))]
    public void TestWishlistsSize(List<Employee> teamLeads, List<Employee> juniors)
    {
        var provider = GetService<IWishlistProvider>();

        var juniorsWishlists = provider.ProvideJuniorsWishlists(juniors, teamLeads);
        var teamLeadsWishlists = provider.ProvideTeamLeadsWishlists(juniors, teamLeads);

        Assert.Equal(teamLeadsWishlists.Count(), teamLeads.Count);
        Assert.Equal(juniorsWishlists.Count(), juniors.Count);
    }

    [Theory]
    [ClassData(typeof(EmployeesGenerator))]
    public void TestWishlistsOwners(List<Employee> teamLeads, List<Employee> juniors)
    {
        var provider = GetService<IWishlistProvider>();

        var juniorsIds = juniors.Select(j => j.Id).ToHashSet();
        var teamLeadsIds = teamLeads.Select(j => j.Id).ToHashSet();

        var juniorsWishlists = provider.ProvideJuniorsWishlists(juniors, teamLeads);
        var teamLeadsWishlists = provider.ProvideTeamLeadsWishlists(juniors, teamLeads);

        Assert.Equal(teamLeadsWishlists.Select(w => w.EmployeeId).ToHashSet(), teamLeadsIds);
        Assert.Equal(juniorsWishlists.Select(w => w.EmployeeId).ToHashSet(), juniorsIds);
    }

    [Theory]
    [ClassData(typeof(EmployeesGenerator))]
    public void TestWishlistsDesiredEmployees(List<Employee> teamLeads, List<Employee> juniors)
    {
        var provider = GetService<IWishlistProvider>();

        var juniorsIds = juniors.Select(j => j.Id).ToHashSet();
        var teamLeadsIds = teamLeads.Select(j => j.Id).ToHashSet();

        var juniorsWishlists = provider.ProvideJuniorsWishlists(juniors, teamLeads);
        var teamLeadsWishlists = provider.ProvideTeamLeadsWishlists(juniors, teamLeads);

        foreach (var tw in teamLeadsWishlists) Assert.Equal(tw.DesiredEmployees.ToHashSet(), juniorsIds);

        foreach (var jw in juniorsWishlists) Assert.Equal(jw.DesiredEmployees.ToHashSet(), teamLeadsIds);
    }
}
