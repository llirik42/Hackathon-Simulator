using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.HrDirector.services.hackathonOrganizer;

public interface IHackathonOrganizer
{
    Hackathon Organize(List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists, List<Team> teams);
}
