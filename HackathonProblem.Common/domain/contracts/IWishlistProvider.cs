using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.domain.contracts;

public interface IWishlistProvider
{
    Wishlist ProvideJuniorWishlist(int juniorId, List<int> teamLeadsIds);

    Wishlist ProviderTeamLeadWishlist(int teamLeadId, List<int> juniorsIds);

    List<Wishlist> ProvideJuniorsWishlists(List<int> juniorsIds, List<int> teamLeadsIds);

    List<Wishlist> ProvideTeamLeadsWishlists(List<int> juniorsIds, List<int> teamLeadsIds);
}
