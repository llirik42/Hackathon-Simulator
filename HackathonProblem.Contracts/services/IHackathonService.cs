using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IHackathonService
{
    List<Employee> GetMembers(int hackathonId);

    List<Employee> GetJuniors(int hackathonId);
    
    List<Employee> GetSeniors(int hackathonId);
    
    List<Team> GetTeams(int hackathonId);
    
    double GetHarmonization(int hackathonId);
    
    List<Hackathon> findHackathon(int hackathonId);
    
    double GetAverageHarmonization();
    
    int SaveHackathon(Hackathon hackathon, List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists);
}
