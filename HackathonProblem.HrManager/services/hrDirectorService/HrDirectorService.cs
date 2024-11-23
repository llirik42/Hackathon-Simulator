using HackathonProblem.Common;
using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.HrManager.models;

namespace HackathonProblem.HrManager.services.hrDirectorService;

public class HrDirectorService(
    IHttpClientFactory factory,
    HrManagerConfig config) : IHrDirector
{
    public double CalculateValue(double[] numbers)
    {
        var httpClient = factory.CreateClient();
        var requestUri = $"{config.HrDirectorConnectionString}/mean";
        var request = new MeanHarmonicRequest { Numbers = numbers };
        return NetworkUtils.PostForEntity<MeanHarmonicRequest, DoubleResponse>(httpClient, requestUri, request).Result
            .value;
    }

    public double CalculateTeamsHarmonization(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        var httpClient = factory.CreateClient();
        var requestUri = $"{config.HrDirectorConnectionString}/teams-harmonization";
        var request = new TeamsHarmonizationRequest
        {
            Teams = teams.ToList(), JuniorsWishlists = juniorsWishlists.ToList(),
            TeamLeadsWishlists = teamLeadsWishlists.ToList()
        };
        return NetworkUtils.PostForEntity<TeamsHarmonizationRequest, DoubleResponse>(httpClient, requestUri, request)
            .Result.value;
    }

    public double CalculateEmployeeHarmonization(int[] desiredEmployees, int desiredEmployeeId)
    {
        var httpClient = factory.CreateClient();
        var requestUri = $"{config.HrDirectorConnectionString}/employee-harmonization";
        var request = new EmployeeHarmonizationRequest
            { DesiredEmployeeId = desiredEmployeeId, DesiredEmployees = desiredEmployees };
        return NetworkUtils.PostForEntity<EmployeeHarmonizationRequest, DoubleResponse>(httpClient, requestUri, request)
            .Result.value;
    }
}
