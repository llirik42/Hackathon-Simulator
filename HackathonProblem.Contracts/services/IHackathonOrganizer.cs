using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IHackathonOrganizer
{
    public Hackathon Organize(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists);
}
