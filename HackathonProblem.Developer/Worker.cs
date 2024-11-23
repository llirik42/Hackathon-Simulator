using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.Developer.models;
using HackathonProblem.Developer.services.hrManagerService;
using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Developer;

public class Worker(
    DeveloperConfig config,
    IEmployeeProvider employeeProvider,
    IWishlistProvider wishlistProvider,
    IHrManagerService hrManagerService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var wishlist = GetWishlist();
        var response = await SendRequest(wishlist);
        Console.WriteLine(response.Detail);
    }

    private Wishlist GetWishlist()
    {
        var myId = config.DeveloperId;
        var juniors = employeeProvider.Provide(config.JuniorsUrl);
        var teamLeads = employeeProvider.Provide(config.TeamLeadsUrl);

        if (config.DeveloperType == DeveloperType.Junior)
        {
            var teamLeadsIds = teamLeads.Select(t => t.Id).ToList();
            return wishlistProvider.ProvideJuniorWishlist(myId, teamLeadsIds);
        }

        var juniorsIds = juniors.Select(t => t.Id).ToList();
        return wishlistProvider.ProviderTeamLeadWishlist(myId, juniorsIds);
    }

    private async Task<DetailResponse> SendRequest(Wishlist wishlist)
    {
        if (config.DeveloperType == DeveloperType.Junior) return await hrManagerService.PostJuniorWishlist(wishlist);

        return await hrManagerService.PostTeamLeadWishlist(wishlist);
    }
}
