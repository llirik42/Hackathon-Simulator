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
    IHrDirectorWrapper hrDirectorWrapper) : IHackathonService
{
    public DetailResponse BuildTeamsAndPost(List<Wishlist> juniorsWishlists,
        List<Wishlist> teamLeadsWishlists)
    {
        var juniors = employeeProvider.Provide(config.JuniorsUrl);
        var teamLeads = employeeProvider.Provide(config.TeamLeadsUrl);
        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists).ToList();
        
        return hrDirectorWrapper.PostHackathonData(teams, juniorsWishlists, teamLeadsWishlists);
    }
}
