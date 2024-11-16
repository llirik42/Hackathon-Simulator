using Hackathon_Simulator.dto;

namespace Hackathon_Simulator.services.impl;

public class HarmonizationCalculatorImpl : IHarmonizationCalculator
{
    public double Calculate(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        var teamsList = teams.ToList();
        var juniorsWishlistsList = juniorsWishlists.ToList();
        var teamLeadsWishlistsList = teamLeadsWishlists.ToList();

        double result = 0;

        foreach (var t in teamsList)
        {
            var juniorId = t.Junior.Id;
            var teamLeadId = t.TeamLead.Id;

            var juniorWishlist = juniorsWishlistsList.First(wishlist => wishlist.EmployeeId == juniorId);
            var teamLeadWishlist = teamLeadsWishlistsList.First(wishlist => wishlist.EmployeeId == teamLeadId);

            var juniorHarmonization = CalculateEmployeeHarmonization(juniorWishlist.DesiredEmployees, teamLeadId);
            var teamLeadHarmonization = CalculateEmployeeHarmonization(teamLeadWishlist.DesiredEmployees, juniorId);

            result += 1.0 / juniorHarmonization + 1.0 / teamLeadHarmonization;
        }

        return 2 * teamsList.Count / result;
    }

    public double CalculateEmployeeHarmonization(int[] desiredEmployees, int desiredEmployeeId)
    {
        return desiredEmployees.Length - Array.IndexOf(desiredEmployees, desiredEmployeeId);
    }
}
