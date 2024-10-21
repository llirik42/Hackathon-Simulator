using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IWishlistProvider
{
    List<Wishlist> ProvideJuniorsWishlists(List<Employee> juniors, List<Employee> teamLeads);

    List<Wishlist> ProvideTeamLeadsWishlists(List<Employee> juniors, List<Employee> teamLeads);
}
