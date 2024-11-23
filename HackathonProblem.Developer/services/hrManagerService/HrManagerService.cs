using HackathonProblem.Common;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;

namespace HackathonProblem.Developer.services.hrManagerService;

public class HrManagerService(IHttpClientFactory factory, HrManagerConfig config) : IHrManagerService
{
    public async Task<DetailResponse> PostJuniorWishlist(Wishlist wishlist)
    {
        return await PostWishlist(wishlist, "/juniors");
    }

    public async Task<DetailResponse> PostTeamLeadWishlist(Wishlist wishlist)
    {
        return await PostWishlist(wishlist, "/teamleads");
    }

    private async Task<DetailResponse> PostWishlist(Wishlist wishlist, string path)
    {
        var httpClient = factory.CreateClient();
        var requestUri = $"{config.ConnectionString}{path}";

        var request = new WishlistRequest
            { EmployeeId = wishlist.EmployeeId, DesiredEmployees = wishlist.DesiredEmployees };

        return await NetworkUtils.PostForDetailResponse(httpClient, requestUri, request);
    }
}
