using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;

namespace HackathonProblem.HrDirector.services.hackathonService;

public interface IHackathonService
{
    void StartNewHackathon();
    
    void ProcessTeams(List<ShortTeam> teams);

    void ProcessJuniorWishlist(Wishlist wishlist);
    
    void ProcessTeamLeadWishlist(Wishlist wishlist);
}
