using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.HrDirector.services.impl;

public class HackathonOrganizer(IHrDirector hrDirector) : IHackathonOrganizer
{
    public Hackathon Organize(List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists, List<Team> teams)
    {
        var harmonization = hrDirector.CalculateTeamsHarmonization(teams, teamLeadsWishlists, juniorsWishlists);
        return new Hackathon(teams, harmonization);
    }
}
