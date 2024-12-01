using HackathonProblem.Common.models;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hackathonService;
using HackathonProblem.HrManager.services.wishlistService;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrManager.controllers;

[Route("juniors")]
public class JuniorsController(
    IWishlistService wishlistService,
    IHackathonService hackathonService,
    HrManagerConfig config)
{
    [HttpPost]
    public DetailResponse PostJuniorWishlist([FromBody] WishlistRequest request)
    {
        wishlistService.AddJuniorWishlist(request.ToWishlist());

        if (wishlistService.MatchWishlistsCount(config.EmployeeCount))
        {
            var response = hackathonService.BuildTeamsAndPost(wishlistService.GetJuniorsWishlists(),
                wishlistService.GetTeamLeadsWishlists());
            Console.WriteLine(response.Detail);
        }

        return new DetailResponse($"Wishlist from junior - {request.EmployeeId} accepted");
    }
}
