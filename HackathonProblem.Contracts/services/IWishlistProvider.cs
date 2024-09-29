using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IWishlistProvider
{
    IEnumerable<Wishlist> ProvideJuniorsWishlists(List<Employee> juniors, List<Employee> teamLeads);

    IEnumerable<Wishlist> ProvideTeamLeadsWishlists(List<Employee> juniors, List<Employee> teamLeads);
}
