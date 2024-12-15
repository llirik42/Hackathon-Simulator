using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.Common.models.responses;

namespace HackathonProblem.Developer.services.hrManagerService;

public interface IHrManagerService
{
    Task<DetailResponse> PostJuniorWishlist(Wishlist wishlist);

    Task<DetailResponse> PostTeamLeadWishlist(Wishlist wishlist);
}
