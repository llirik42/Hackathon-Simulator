using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.HrDirector.domain;

public class HrDirector : IHrDirector
{
    public double CalculateValue(double[] numbers)
    {
        var result = numbers.Sum(n => 1 / n);

        return numbers.Length / result;
    }

    public double CalculateTeamsHarmonization(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        var teamsList = teams.ToList();
        var juniorsWishlistsList = juniorsWishlists.ToList();
        var teamLeadsWishlistsList = teamLeadsWishlists.ToList();

        var harmonizationValues = new List<double>();

        foreach (var t in teamsList)
        {
            var juniorId = t.Junior.Id;
            var teamLeadId = t.TeamLead.Id;

            var juniorWishlist = juniorsWishlistsList.First(wishlist => wishlist.EmployeeId == juniorId);
            var teamLeadWishlist = teamLeadsWishlistsList.First(wishlist => wishlist.EmployeeId == teamLeadId);

            var juniorHarmonization = CalculateEmployeeHarmonization(juniorWishlist.DesiredEmployees, teamLeadId);
            var teamLeadHarmonization = CalculateEmployeeHarmonization(teamLeadWishlist.DesiredEmployees, juniorId);

            harmonizationValues.Add(juniorHarmonization);
            harmonizationValues.Add(teamLeadHarmonization);
        }

        return CalculateValue(harmonizationValues.ToArray());
    }

    public double CalculateEmployeeHarmonization(int[] desiredEmployees, int desiredEmployeeId)
    {
        return desiredEmployees.Length - Array.IndexOf(desiredEmployees, desiredEmployeeId);
    }
}
