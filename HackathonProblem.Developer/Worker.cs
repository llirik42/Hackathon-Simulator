using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.Common.models.responses;
using HackathonProblem.Developer.models;
using HackathonProblem.Developer.services.hrManagerService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HackathonProblem.Developer;

public class Worker(
    DeveloperConfig config,
    IEmployeeProvider employeeProvider,
    IWishlistProvider wishlistProvider,
    IHrManagerService hrManagerService,
    ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var wishlist = GetWishlist();
        var response = await SendRequest(wishlist);
        logger.LogInformation("Received response from manager: \"{Response}\"", response.Detail);
    }

    private Wishlist GetWishlist()
    {
        var myId = config.Id;
        var juniors = employeeProvider.Provide(config.JuniorsUrl);
        var teamLeads = employeeProvider.Provide(config.TeamLeadsUrl);

        if (config.Type == DeveloperType.Junior)
        {
            var teamLeadsIds = teamLeads.Select(t => t.Id).ToList();
            return wishlistProvider.ProvideJuniorWishlist(myId, teamLeadsIds);
        }

        var juniorsIds = juniors.Select(t => t.Id).ToList();
        return wishlistProvider.ProviderTeamLeadWishlist(myId, juniorsIds);
    }

    private async Task<DetailResponse> SendRequest(Wishlist wishlist)
    {
        if (config.Type == DeveloperType.Junior) return await hrManagerService.PostJuniorWishlist(wishlist);

        return await hrManagerService.PostTeamLeadWishlist(wishlist);
    }
}
