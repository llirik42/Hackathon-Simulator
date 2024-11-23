using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.HrDirector.services;

public interface IHackathonOrganizer
{
    Hackathon Organize(List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists, List<Team> teams);
}
