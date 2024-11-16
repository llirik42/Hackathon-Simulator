using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.domain.contracts;

public interface IHrDirector
{
    double CalculateValue(double[] numbers);

    double CalculateTeamsHarmonization(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists);

    double CalculateEmployeeHarmonization(int[] desiredEmployees, int desiredEmployeeId);
}
