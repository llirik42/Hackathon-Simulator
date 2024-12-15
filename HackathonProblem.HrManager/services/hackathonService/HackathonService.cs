using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hrDirectorService.wrapper;

namespace HackathonProblem.HrManager.services.hackathonService;

public class HackathonService(
    IEmployeeProvider employeeProvider,
    HrManagerConfig config,
    IHrManager hrManager,
    IHrDirectorWrapper hrDirectorWrapper, 
    ILogger<HackathonService> logger) : IHackathonService
{
    public DetailResponse BuildTeamsAndPost(List<Wishlist> allJuniorsWishlists,
        List<Wishlist> allTeamLeadsWishlists)
    {
        var allJuniors = employeeProvider.Provide(config.JuniorsUrl);
        var allTeamLeads = employeeProvider.Provide(config.TeamLeadsUrl);

        // Juniors and team leads presented in wishlists
        var juniors = allJuniorsWishlists.Select(w => w.EmployeeId)
            .Select(juniorId => allJuniors.Single(y => y.Id == juniorId)).ToList();
        var teamLeads = allTeamLeadsWishlists.Select(w => w.EmployeeId)
            .Select(teamLeadId => allTeamLeads.Single(y => y.Id == teamLeadId)).ToList();
        
        var teams = hrManager.BuildTeams(teamLeads, juniors, allTeamLeadsWishlists, allJuniorsWishlists).ToList();
        
        var response = hrDirectorWrapper.PostHackathonData(teams, allJuniorsWishlists, allTeamLeadsWishlists);
        logger.LogInformation("Received response from director: \"{Response}\"", response.Detail);
        
        return response;
    }
}
