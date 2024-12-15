using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models.responses;

namespace HackathonProblem.HrManager.services.hackathonService;

public interface IHackathonService
{
    DetailResponse BuildTeamsAndPost(List<Wishlist> juniorsWishlists, List<Wishlist> teamLeadsWishlists);
}
