using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IDbService
{
    bool CreateJunior(Employee junior);

    bool CreateTeamLead(Employee teamLead);

    void AddJuniorWishlists(int hackathonId, List<Wishlist> juniorWishlists);

    void AddTeamLeadWishlists(int hackathonId, List<Wishlist> teamLeadWishlists);

    void AddTeams(int hackathonId, List<Team> teams);

    public int CreateHackathon(double harmonization);

    double GetAverageHarmonization();

    Hackathon? FindHackathon(int hackathonId);
}
