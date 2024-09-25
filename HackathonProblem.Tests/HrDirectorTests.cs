using HackathonProblem.Contracts;
using Xunit;

namespace HackathonProblem.Tests;

public class HrDirectorTests : GlobalFixture
{
    [Fact]
    public void TestAlgorithm()
    {
        var calculator = GetService<IHarmonizationCalculator>();

        Assert.Equal(1, Math.Round(calculator.CalculateValue([1])));
        Assert.Equal(1, Math.Round(calculator.CalculateValue([1, 1, 1])));
        Assert.Equal(2, Math.Round(calculator.CalculateValue([2, 2, 2])));
        Assert.Equal(7, Math.Round(calculator.CalculateValue([7, 7, 7, 7, 7, 7])));
        Assert.Equal(2, Math.Round(calculator.CalculateValue([1, 3, 6])));
        Assert.Equal(1.92, Math.Round(calculator.CalculateValue([1, 2, 3, 4]), 2));
        Assert.Equal(3.41, Math.Round(calculator.CalculateValue([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]), 2));
    }

    [Fact]
    public void TestTeamsHarmonization1()
    {
        const int count = 5;
        var teamLeads = GetSimpleEmployees(count);
        var juniors = GetSimpleEmployees(count);

        List<Wishlist> teamLeadsWishlists =
        [
            new(1, [4, 5, 3, 2, 1]),
            new(2, [3, 5, 2, 4, 1]),
            new(3, [2, 3, 4, 1, 5]),
            new(4, [1, 5, 4, 3, 2]),
            new(5, [5, 4, 3, 1, 2])
        ];

        List<Wishlist> juniorsWishlists =
        [
            new(1, [3, 5, 4, 2, 1]),
            new(2, [1, 3, 2, 5, 4]),
            new(3, [4, 1, 3, 2, 5]),
            new(4, [3, 4, 1, 2, 5]),
            new(5, [4, 2, 3, 1, 5])
        ];

        List<Team> teams =
        [
            new(teamLeads[0], juniors[4]),
            new(teamLeads[4], juniors[2]),
            new(teamLeads[3], juniors[0]),
            new(teamLeads[1], juniors[3]),
            new(teamLeads[2], juniors[1])
        ];

        var calculator = GetService<IHarmonizationCalculator>();

        Assert.Equal(2.459,
            Math.Round(calculator.CalculateTeamsHarmonization(teams, teamLeadsWishlists, juniorsWishlists), 3));
    }

    [Fact]
    public void TestTeamsHarmonization2()
    {
        const int count = 10;
        var teamLeads = GetSimpleEmployees(count);
        var juniors = GetSimpleEmployees(count);

        List<Wishlist> teamLeadsWishlists =
        [
            new(1, [8, 4, 2, 10, 1, 5, 9, 6, 3, 7]),
            new(2, [8, 1, 2, 6, 5, 4, 9, 3, 10, 7]),
            new(3, [1, 5, 4, 9, 10, 8, 6, 7, 2, 3]),
            new(4, [10, 9, 1, 2, 5, 8, 6, 7, 4, 3]),
            new(5, [1, 4, 5, 7, 10, 6, 2, 8, 3, 9]),
            new(6, [7, 6, 1, 10, 5, 4, 2, 8, 3, 9]),
            new(7, [7, 9, 3, 5, 1, 8, 10, 6, 2, 4]),
            new(8, [6, 10, 5, 7, 1, 9, 2, 8, 4, 3]),
            new(9, [8, 2, 6, 7, 1, 4, 10, 3, 5, 9]),
            new(10, [8, 3, 7, 4, 5, 1, 10, 9, 2, 6])
        ];

        List<Wishlist> juniorsWishlists =
        [
            new(1, [7, 1, 4, 9, 10, 6, 2, 5, 8, 3]),
            new(2, [4, 3, 8, 9, 6, 5, 2, 10, 1, 7]),
            new(3, [9, 7, 5, 2, 8, 3, 4, 1, 6, 10]),
            new(4, [9, 4, 5, 7, 8, 6, 10, 3, 2, 1]),
            new(5, [3, 6, 4, 8, 7, 5, 9, 10, 2, 1]),
            new(6, [9, 2, 3, 7, 4, 10, 6, 1, 5, 8]),
            new(7, [4, 10, 2, 9, 8, 1, 7, 3, 5, 6]),
            new(8, [7, 5, 1, 2, 8, 6, 4, 10, 9, 3]),
            new(9, [8, 4, 3, 2, 1, 9, 5, 10, 6, 7]),
            new(10, [3, 9, 7, 2, 10, 4, 1, 6, 8, 5])
        ];

        List<Team> teams =
        [
            new(teamLeads[1], juniors[0]),
            new(teamLeads[8], juniors[1]),
            new(teamLeads[9], juniors[2]),
            new(teamLeads[4], juniors[3]),
            new(teamLeads[2], juniors[4]),
            new(teamLeads[5], juniors[5]),
            new(teamLeads[7], juniors[6]),
            new(teamLeads[0], juniors[7]),
            new(teamLeads[6], juniors[8]),
            new(teamLeads[3], juniors[9])
        ];

        var calculator = GetService<IHarmonizationCalculator>();

        Assert.Equal(4.464,
            Math.Round(calculator.CalculateTeamsHarmonization(teams, teamLeadsWishlists, juniorsWishlists), 3));
    }
}
