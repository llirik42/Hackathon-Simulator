using HackathonProblem.Common;
using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.mapping;
using HackathonProblem.Common.models;

namespace HackathonProblem.HrManager.services.hrDirectorService.wrapper;

public class HrDirectorWrapper(IHrDirector hrDirector, IHttpClientFactory factory, HrDirectorConfig config, TeamMapper teamMapper) : IHrDirectorWrapper
{
    public double CalculateValue(double[] numbers)
    {
        return hrDirector.CalculateValue(numbers);
    }

    public double CalculateTeamsHarmonization(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        return hrDirector.CalculateTeamsHarmonization(teams, teamLeadsWishlists, juniorsWishlists);
    }

    public double CalculateEmployeeHarmonization(int[] desiredEmployees, int desiredEmployeeId)
    {
        return hrDirector.CalculateEmployeeHarmonization(desiredEmployees, desiredEmployeeId);
    }

    public DetailResponse PostHackathonData(List<Team> teams, List<Wishlist> juniorsWishlists, List<Wishlist> teamLeadsWishlists)
    {
        var httpClient = factory.CreateClient();
        var requestUri = $"{config.HrDirectorConnectionString}/hackathons";
        var request = new HackathonDataRequest
        {
            Teams = teams.Select(teamMapper.TeamToShortTeam).ToList(),
            JuniorsWishlists = juniorsWishlists,
            TeamLeadsWishlists = teamLeadsWishlists
        };

        return NetworkUtils.PostForEntity<HackathonDataRequest, DetailResponse>(httpClient, requestUri, request).Result;
    }
}
