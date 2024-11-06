using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IHarmonizationCalculator
{
    double CalculateValue(double[] numbers);

    double CalculateTeamsHarmonization(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists);

    double CalculateEmployeeHarmonization(int[] desiredEmployees, int desiredEmployeeId);
}
