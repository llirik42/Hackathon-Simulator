using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface ITeamLeadWishlistService
{
    void AddTeamLeadWishlist(int hackathonId, Wishlist teamLeadWishlist);
}
