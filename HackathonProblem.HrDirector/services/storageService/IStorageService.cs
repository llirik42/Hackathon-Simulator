using HackathonProblem.Common.domain.entities;
using HackathonProblem.HrDirector.models;

namespace HackathonProblem.HrDirector.services.storageService;

public interface IStorageService
{
    bool CreateJunior(Employee junior);

    bool CreateTeamLead(Employee teamLead);

    void AddJuniorWishlists(int hackathonId, List<Wishlist> juniorWishlists);

    void AddTeamLeadWishlists(int hackathonId, List<Wishlist> teamLeadWishlists);

    void AddTeams(int hackathonId, List<Team> teams);

    public CreatedHackathon CreateHackathon(double harmonization);

    double GetAverageHarmonization();

    Hackathon? FindHackathon(int hackathonId);
}
