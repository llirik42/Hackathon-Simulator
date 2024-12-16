using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.HrManager.services.wishlistService;

public interface IWishlistService
{
    void AddJuniorWishlist(Wishlist wishlist);

    void AddTeamLeadWishlist(Wishlist wishlist);

    List<Wishlist> PopJuniorsWishlists();

    List<Wishlist> PopTeamLeadsWishlists();

    bool MatchJuniorsWishlistsCount(int count);
    
    bool MatchTeamLeadsWishlistsCount(int count);
}
