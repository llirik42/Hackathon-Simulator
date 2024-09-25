using HackathonProblem.Contracts;

namespace HackathonProblem.HackathonOrganizer;

public class HackathonOrganizer(IHarmonizationCalculator calculator, ITeamBuildingStrategy strategy)
    : IHackathonOrganizer
{
    public HackathonMembers Organize(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        var teamLeadsWishlistsList = teamLeadsWishlists.ToList();
        var juniorsWishlistsList = juniorsWishlists.ToList();

        var teams = strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlistsList, juniorsWishlistsList);
        var teamsList = teams.ToList();
        var harmonization = calculator.CalculateTeamsHarmonization(teamsList, teamLeadsWishlistsList, juniorsWishlistsList);

        return new HackathonMembers(teamsList, harmonization);
    }
}
