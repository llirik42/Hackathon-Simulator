namespace HackathonProblem.Contracts;

public interface IHackathonOrganizer
{
    public HackathonMembers Organize(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, 
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists);
}
