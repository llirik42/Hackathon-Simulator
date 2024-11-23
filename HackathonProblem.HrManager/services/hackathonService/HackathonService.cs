using HackathonProblem.Common;
using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.mapping;
using HackathonProblem.Common.models;
using HackathonProblem.HrManager.models;

namespace HackathonProblem.HrManager.services.hackathonService;

public class HackathonService(
    IHttpClientFactory factory,
    IEmployeeProvider employeeProvider,
    HrManagerConfig config,
    IHrManager hrManager,
    TeamMapper teamMapper) : IHackathonService
{
    public async Task<DetailResponse> BuildTeamsAndPost(List<Wishlist> juniorsWishlists,
        List<Wishlist> teamLeadsWishlists)
    {
        var juniors = employeeProvider.Provide(config.JuniorsUrl);
        var teamLeads = employeeProvider.Provide(config.TeamLeadsUrl);

        var teams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists).ToList();

        return await PostHackathonData(teams, juniorsWishlists, teamLeadsWishlists);
    }

    private async Task<DetailResponse> PostHackathonData(List<Team> teams, List<Wishlist> juniorsWishlists,
        List<Wishlist> teamLeadsWishlists)
    {
        var httpClient = factory.CreateClient();
        var requestUri = $"{config.HrDirectorConnectionString}/teams";
        var request = new HackathonDataRequest
        {
            Teams = teams.Select(teamMapper.TeamToShortTeam).ToList(),
            JuniorsWishlists = juniorsWishlists,
            TeamLeadsWishlists = teamLeadsWishlists
        };

        return await NetworkUtils.PostForEntity<HackathonDataRequest, DetailResponse>(httpClient, requestUri, request);
    }
}
