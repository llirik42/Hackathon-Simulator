using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.Common.models.message;
using HackathonProblem.HrDirector.services.hackathonService;
using MassTransit;

namespace HackathonProblem.HrDirector.consumers;

public class WishlistDeclarationConsumer(
    IHackathonService hackathonService,
    ILogger<WishlistDeclarationConsumer> logger) : IConsumer<WishlistDeclaration>
{
    public Task Consume(ConsumeContext<WishlistDeclaration> context)
    {
        var message = context.Message;
        logger.LogInformation("Received wishlist from {DeveloperType}-{DeveloperId} for hackathon-{Hackathon}",
            message.DeveloperType, message.DeveloperId, message.HackathonId);
        var wishlist = new Wishlist(message.DeveloperId, message.DesiredEmployees);

        if (message.DeveloperType == DeveloperType.Junior)
            hackathonService.ProcessJuniorWishlist(wishlist);
        else
            hackathonService.ProcessTeamLeadWishlist(wishlist);

        return Task.CompletedTask;
    }
}
