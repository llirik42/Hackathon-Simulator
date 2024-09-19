using Contracts;

namespace HackathonOrganizer;

public class HackathonOrganizer(
    IHarmonizationCalculator calculator,
    ITeamBuildingStrategy strategy,
    IWishlistProvider wishlistProvider)
    : IHackathonOrganizer
{
    public HackathonMembers Organize(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors)
    {
        var teamLeadsList = teamLeads.ToList();
        var juniorsList = juniors.ToList();

        var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniorsList, teamLeadsList).ToList();
        var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniorsList, teamLeadsList).ToList();

        var teams = strategy.BuildTeams(teamLeadsList, juniorsList, teamLeadsWishlists, juniorsWishlists);
        var teamsList = teams.ToList();
        var harmonization = calculator.Calculate(teamsList, teamLeadsWishlists, juniorsWishlists);

        return new HackathonMembers(teamsList, harmonization);
    }
}
