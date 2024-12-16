using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.HrManager.services.wishlistService;

public interface IWishlistService
{
    void AddJuniorWishlist(Wishlist wishlist);

    void AddTeamLeadWishlist(Wishlist wishlist);

    List<Wishlist> GetJuniorsWishlists();

    List<Wishlist> GetTeamLeadsWishlists();

    bool MatchJuniorsWishlistsCount(int count);
    
    bool MatchTeamLeadsWishlistsCount(int count);
}
