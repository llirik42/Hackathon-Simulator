using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.domain.contracts;

public interface IHrManager
{
    IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists);
}
