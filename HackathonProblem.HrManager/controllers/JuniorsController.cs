using HackathonProblem.Common.models;
using HackathonProblem.Common.models.requests;
using HackathonProblem.Common.models.responses;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hackathonService;
using HackathonProblem.HrManager.services.wishlistService;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrManager.controllers;

[Route("juniors")]
public class JuniorsController(
    IWishlistService wishlistService,
    IHackathonService hackathonService,
    HrManagerConfig config,
    Locker locker)
{
    [HttpPost]
    public DetailResponse PostJuniorWishlist([FromBody] WishlistRequest request)
    {
        wishlistService.AddJuniorWishlist(request.ToWishlist());
        
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

        return new DetailResponse($"Wishlist from junior - {request.EmployeeId} accepted");
    }
}
