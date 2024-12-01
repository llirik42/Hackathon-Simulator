using System.Collections.Concurrent;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;

namespace HackathonProblem.HrManager.services.wishlistService;

public class ConcurrentWishlistService : IWishlistService
{
    private record DeveloperWishlist(Wishlist Wishlist, DeveloperType DeveloperType);
    
    private readonly ConcurrentQueue<DeveloperWishlist> _wishlists = new();
    
    public void AddJuniorWishlist(Wishlist wishlist)
    {
        _wishlists.Enqueue(new DeveloperWishlist(wishlist, DeveloperType.Junior));
    }

    public void AddTeamLeadWishlist(Wishlist wishlist)
    {
        _wishlists.Enqueue(new DeveloperWishlist(wishlist, DeveloperType.TeamLead));
    }

    public List<Wishlist> GetJuniorsWishlists()
    {
        var juniorsWishlists = _wishlists.Where(w => w.DeveloperType == DeveloperType.Junior);
        var wishlists = juniorsWishlists.Select(e => e.Wishlist);
        return wishlists.ToList();
    }

    public List<Wishlist> GetTeamLeadsWishlists()
    {
        var juniorsWishlists = _wishlists.Where(w => w.DeveloperType == DeveloperType.TeamLead);
        var wishlists = juniorsWishlists.Select(e => e.Wishlist);
        return wishlists.ToList();
    }

    public bool MatchWishlistsCount(int count)
    {
        return _wishlists.Count == count * 2;
    }
}
