using Hackathon_Simulator.dto;

namespace Hackathon_Simulator.services;

public interface IWishlistProvider
{
    IEnumerable<Wishlist> ProvideJuniorsWishlists();

    IEnumerable<Wishlist> ProvideTeamLeadsWishlists();
}
