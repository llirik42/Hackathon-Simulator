namespace Contracts;

public interface IWishlistProvider
{
    IEnumerable<Wishlist> ProvideJuniorsWishlists();

    IEnumerable<Wishlist> ProvideTeamLeadsWishlists();
}
