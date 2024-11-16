using Hackathon_Simulator.dto;

namespace Hackathon_Simulator.services;

public interface IHarmonizationCalculator
{
    double Calculate(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists);

    double CalculateEmployeeHarmonization(int[] desiredEmployees, int desiredEmployeeId);
}
