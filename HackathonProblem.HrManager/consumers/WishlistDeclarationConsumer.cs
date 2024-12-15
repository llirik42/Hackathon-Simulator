using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.Common.models.message;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hackathonService;
using HackathonProblem.HrManager.services.wishlistService;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HackathonProblem.HrManager.consumers;

public class WishlistDeclarationConsumer(
    IWishlistService wishlistService,
    IHackathonService hackathonService,
    HrManagerConfig config,
    Locker locker,
    ILogger<WishlistDeclarationConsumer> logger) : IConsumer<WishlistDeclaration>
{
    public Task Consume(ConsumeContext<WishlistDeclaration> context)
    {
        var message = context.Message;
        SaveWishlist(message);
       
        lock (locker)
        {
            var matchJuniors = wishlistService.MatchJuniorsWishlistsCount(config.EmployeeCount);
            var matchTeamLeads = wishlistService.MatchTeamLeadsWishlistsCount(config.EmployeeCount);

            if (!matchJuniors || !matchTeamLeads) return Task.CompletedTask;
            
            var response = hackathonService.BuildTeamsAndPost(wishlistService.PopJuniorsWishlists(),
                wishlistService.PopTeamLeadsWishlists());
            logger.LogInformation("Received response from director: {Response}", response.Detail);
        }

        return Task.CompletedTask;
    }

    private void SaveWishlist(WishlistDeclaration message)
    {
        var wishlist = new Wishlist(message.DeveloperId, message.DesiredEmployees);

        if (message.DeveloperType == DeveloperType.Junior)
        {
            wishlistService.AddJuniorWishlist(wishlist);
            return;
        }

        wishlistService.AddTeamLeadWishlist(wishlist);
    }
}
