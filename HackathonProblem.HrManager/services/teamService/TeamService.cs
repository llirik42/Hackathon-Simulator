using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models.responses;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hrDirectorService.wrapper;

namespace HackathonProblem.HrManager.services.teamService;

public class TeamService(
    IEmployeeProvider employeeProvider,
    HrManagerConfig config,
    IHrManager hrManager,
    IHrDirectorWrapper hrDirectorWrapper) : ITeamService
{
    public DetailResponse BuildTeamsAndPost(int hackathonId, List<Wishlist> juniorsWishlists, List<Wishlist> teamLeadsWishlists)
    {
        var juniors = employeeProvider.Provide(config.JuniorsUrl);
        var teamLeads = employeeProvider.Provide(config.TeamLeadsUrl);
        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists).ToList();
        return hrDirectorWrapper.PostTeams(teams, hackathonId);
    }
}
