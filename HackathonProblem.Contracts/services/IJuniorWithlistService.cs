using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IJuniorWithlistService
{
    void AddJuniorWishlist(int hackathonId, Wishlist juniorWishlist);
}
