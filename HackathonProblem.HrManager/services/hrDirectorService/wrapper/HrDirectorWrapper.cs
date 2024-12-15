using HackathonProblem.Common;
using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.mapping;
using HackathonProblem.Common.models.requests;
using HackathonProblem.Common.models.responses;

namespace HackathonProblem.HrManager.services.hrDirectorService.wrapper;

public class HrDirectorWrapper(
    IHrDirector hrDirector,
    IHttpClientFactory factory,
    HrDirectorConfig config,
    TeamMapper teamMapper) : IHrDirectorWrapper
{
    public double CalculateValue(double[] numbers)
    {
        return hrDirector.CalculateValue(numbers);
    }

    public double CalculateTeamsHarmonization(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        return hrDirector.CalculateTeamsHarmonization(teams, teamLeadsWishlists, juniorsWishlists);
    }

    public double CalculateEmployeeHarmonization(int[] desiredEmployees, int desiredEmployeeId)
    {
        return hrDirector.CalculateEmployeeHarmonization(desiredEmployees, desiredEmployeeId);
    }

    public DetailResponse PostTeams(List<Team> teams)
    {
        var httpClient = factory.CreateClient();
        var requestUri = $"{config.ConnectionString}/teams";
        var request = new TeamsRequest
        {
            Teams = teams.Select(teamMapper.TeamToShortTeam).ToList()
        };

        return NetworkUtils.PostForEntity<TeamsRequest, DetailResponse>(httpClient, requestUri, request).Result;
    }
}
