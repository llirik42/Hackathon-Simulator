using Contracts;

namespace HackathonOrganizer;

public class HackathonOrganizer : IHackathonOrganizer
{
    private readonly IHarmonizationCalculator _calculator;
    private readonly ITeamBuildingStrategy _strategy;

    public HackathonOrganizer()
    {
        _calculator = new HrDirector.HrDirector();
        _strategy = new HrManager.HrManager(_calculator);
    }

    public HackathonMembers Organize(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        var teamLeadsWishlistsList = teamLeadsWishlists.ToList();
        var juniorsWishlistsList = juniorsWishlists.ToList();

        var teams = _strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlistsList, juniorsWishlistsList);
        var teamsList = teams.ToList();
        var harmonization = _calculator.Calculate(teamsList, teamLeadsWishlistsList, juniorsWishlistsList);

        return new HackathonMembers(teamsList, harmonization);
    }
}
