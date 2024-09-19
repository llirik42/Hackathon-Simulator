using System.Collections;
using Contracts;
using Xunit;

namespace Tests;

public class EmployeesGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
    [
        new object[]
        {
            new List<Employee> { new(1, "Team-lead 1") },
            new List<Employee> { new(1, "Junior 1") }
        },
        new object[]
        {
            new List<Employee>
            {
                new(1, "Team-lead 1"),
                new(2, "Team-lead 2")
            },
            new List<Employee> { new(13, "Junior 13") }
        },
        new object[]
        {
            new List<Employee>
            {
                new(1, "Team-lead 1"),
                new(2, "Team-lead 2")
            },
            new List<Employee>
            {
                new(6, "Junior 6"),
                new(17, "Junior 17")
            }
        },
        new object[]
        {
            new List<Employee>
            {
                new(1, "Team-lead 1"),
                new(2, "Team-lead 2"),
                new(3, "Team-lead 3"),
                new(4, "Team-lead 4")
            },
            new List<Employee>
            {
                new(101, "Junior 101"),
                new(102, "Junior 102"),
                new(103, "Junior 103"),
                new(104, "Junior 104"),
                new(105, "Junior 105"),
                new(106, "Junior 106"),
                new(107, "Junior 107")
            }
        }
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class WishlistsProviderTests : GlobalFixture
{
    [Theory]
    [ClassData(typeof(EmployeesGenerator))]
    public void TestWishlistsSize(List<Employee> teamLeads, List<Employee> juniors)
    {
        var provider = GetService<IWishlistProvider>();
        
        var juniorsWishlists = provider.ProvideJuniorsWishlists(juniors, teamLeads).ToList();
        var teamLeadsWishlists = provider.ProvideTeamLeadsWishlists(juniors, teamLeads).ToList();
        
        Assert.Equal(teamLeadsWishlists.Count, teamLeads.Count);
        Assert.Equal(juniorsWishlists.Count, juniors.Count);
    }

    [Theory]
    [ClassData(typeof(EmployeesGenerator))]
    public void TestWishlistsOwners(List<Employee> teamLeads, List<Employee> juniors)
    {
        var provider = GetService<IWishlistProvider>();
        
        var juniorsIds = juniors.Select(j => j.Id).ToHashSet();
        var teamLeadsIds = teamLeads.Select(j => j.Id).ToHashSet();
        
        var juniorsWishlists = provider.ProvideJuniorsWishlists(juniors, teamLeads).ToList();
        var teamLeadsWishlists = provider.ProvideTeamLeadsWishlists(juniors, teamLeads).ToList();
        
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
        
        var juniorsWishlists = provider.ProvideJuniorsWishlists(juniors, teamLeads).ToList();
        var teamLeadsWishlists = provider.ProvideTeamLeadsWishlists(juniors, teamLeads).ToList();
        
        foreach (var tw in teamLeadsWishlists)
        {
            Assert.Equal(tw.DesiredEmployees.ToHashSet(), juniorsIds);
        }
        
        foreach (var jw in juniorsWishlists)
        {
            Assert.Equal(jw.DesiredEmployees.ToHashSet(), teamLeadsIds);
        }
    }
}
