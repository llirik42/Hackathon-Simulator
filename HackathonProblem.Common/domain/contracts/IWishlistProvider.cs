using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.domain.contracts;

public interface IWishlistProvider
{
    List<Wishlist> ProvideJuniorsWishlists(List<Employee> juniors, List<Employee> teamLeads);

    List<Wishlist> ProvideTeamLeadsWishlists(List<Employee> juniors, List<Employee> teamLeads);
}
