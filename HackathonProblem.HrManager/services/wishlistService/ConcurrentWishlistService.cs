using System.Collections.Concurrent;
using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.HrManager.services.wishlistService;

public class ConcurrentWishlistService : IWishlistService
{
    private readonly ConcurrentQueue<Wishlist> _juniorsWishlists = new();
    
    private readonly ConcurrentQueue<Wishlist> _teamLeadsWishlists = new();
    
    public void AddJuniorWishlist(Wishlist wishlist)
    {
        _juniorsWishlists.Enqueue(wishlist);
    }

    public void AddTeamLeadWishlist(Wishlist wishlist)
    {
        _teamLeadsWishlists.Enqueue(wishlist);
    }

    public List<Wishlist> GetJuniorsWishlists()
    {
        return _juniorsWishlists.ToList();
    }

    public List<Wishlist> GetTeamLeadsWishlists()
    {
        return _teamLeadsWishlists.ToList();
    }

    public bool MatchJuniorsWishlistsCount(int count)
    {
        return _juniorsWishlists.Count == count;
    }

    public bool MatchTeamLeadsWishlistsCount(int count)
    {
        return _teamLeadsWishlists.Count == count;
    }
}
