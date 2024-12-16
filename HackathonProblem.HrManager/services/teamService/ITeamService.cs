using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models.responses;

namespace HackathonProblem.HrManager.services.teamService;

public interface ITeamService
{
    DetailResponse BuildTeamsAndPost(int hackathonId, List<Wishlist> juniorsWishlists, List<Wishlist> teamLeadsWishlists);
}
