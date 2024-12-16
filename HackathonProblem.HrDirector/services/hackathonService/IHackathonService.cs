using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;

namespace HackathonProblem.HrDirector.services.hackathonService;

public interface IHackathonService
{
    void SetCurrentHackathonId(int hackathonId);
    
    void ProcessTeams(List<ShortTeam> teams);

    void ProcessJuniorWishlist(Wishlist wishlist);
    
    void ProcessTeamLeadWishlist(Wishlist wishlist);
}
