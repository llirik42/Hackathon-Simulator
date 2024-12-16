using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.Common.models.message;
using HackathonProblem.Developer.models;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HackathonProblem.Developer.consumers;

public class HackathonDeclarationConsumer(
    DeveloperConfig config,
    IEmployeeProvider employeeProvider,
    IWishlistProvider wishlistProvider,
    IBus bus,
    ILogger<HackathonDeclarationConsumer> logger) : IConsumer
{
    public Task Consume(ConsumeContext<HackathonDeclaration> context)
    {
        var hackathonId = context.Message.HackathonId;
        logger.LogInformation("Received message about new hackathon {HackathonId}", hackathonId);
        var wishlist = GetWishlist();
        bus.Publish(new WishlistDeclaration
        {
            HackathonId = hackathonId, DeveloperType = config.Type, DeveloperId = config.Id,
            DesiredEmployees = wishlist.DesiredEmployees
        });

        return Task.CompletedTask;
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
}
