using HackathonProblem.Common.models;
using HackathonProblem.Common.models.requests;
using HackathonProblem.Common.models.responses;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hackathonService;
using HackathonProblem.HrManager.services.wishlistService;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrManager.controllers;

[Route("team-leads")]
public class TeamLeadsController(
    IWishlistService wishlistService,
    IHackathonService hackathonService,
    HrManagerConfig config,
    Locker locker
    )
{
    [HttpPost]
    public DetailResponse PostTeamLeadWishlist([FromBody] WishlistRequest request)
    {
        wishlistService.AddTeamLeadWishlist(request.ToWishlist());

        lock (locker)
        {
            var matchJuniors = wishlistService.MatchJuniorsWishlistsCount(config.EmployeeCount);
            var matchTeamLeads = wishlistService.MatchTeamLeadsWishlistsCount(config.EmployeeCount);
            
            if (matchJuniors && matchTeamLeads)
            {
                hackathonService.BuildTeamsAndPost(wishlistService.PopJuniorsWishlists(),
                    wishlistService.PopTeamLeadsWishlists());
            }
        }
        
        return new DetailResponse($"Wishlist from team-lead - {request.EmployeeId} accepted");
    }
}
