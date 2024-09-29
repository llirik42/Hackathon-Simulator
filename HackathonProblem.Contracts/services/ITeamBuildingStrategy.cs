using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface ITeamBuildingStrategy
{
    IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists);
}
