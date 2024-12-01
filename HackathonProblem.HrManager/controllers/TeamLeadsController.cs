using HackathonProblem.Common.models;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hackathonService;
using HackathonProblem.HrManager.services.wishlistService;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrManager.controllers;

[Route("team-leads")]
public class TeamLeadsController(
    IWishlistService wishlistService,
    IHackathonService hackathonService,
    HrManagerConfig config)
{
    [HttpPost]
    public DetailResponse PostTeamLeadWishlist([FromBody] WishlistRequest request)
    {
        wishlistService.AddTeamLeadWishlist(request.ToWishlist());

        if (wishlistService.MatchWishlistsCount(config.EmployeeCount))
        {
            var response = hackathonService.BuildTeamsAndPost(wishlistService.GetJuniorsWishlists(),
                wishlistService.GetTeamLeadsWishlists());
            Console.WriteLine(response.Detail);
        }
        
        return new DetailResponse($"Wishlist from team-lead - {request.EmployeeId} accepted");
    }
}
