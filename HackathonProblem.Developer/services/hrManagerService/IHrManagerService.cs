using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;

namespace HackathonProblem.Developer.services.hrManagerService;

public interface IHrManagerService
{
    Task<DetailResponse> PostJuniorWishlist(Wishlist wishlist);

    Task<DetailResponse> PostTeamLeadWishlist(Wishlist wishlist);
}
