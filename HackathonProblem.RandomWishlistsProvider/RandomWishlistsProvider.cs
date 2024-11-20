using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.RandomWishlistsProvider;

public class RandomWishlistsProvider(int seed = 0) : IWishlistProvider
{
    private readonly Random _random = new(seed);

    public Wishlist ProvideJuniorWishlist(int juniorId, List<int> teamLeadsIds)
    {
        return ProvideWishlists([juniorId], teamLeadsIds)[0];
    }

    public Wishlist ProviderTeamLeadWishlist(int teamLeadId, List<int> juniorsIds)
    {
        return ProvideWishlists([teamLeadId], juniorsIds)[0];
    }

    public List<Wishlist> ProvideJuniorsWishlists(List<int> juniorsIds, List<int> teamLeadsIds)
    {
        return ProvideWishlists(juniorsIds, teamLeadsIds);
    }

    public List<Wishlist> ProvideTeamLeadsWishlists(List<int> juniorsIds, List<int> teamLeadsIds)
    {
        return ProvideWishlists(teamLeadsIds, juniorsIds);
    }

    private List<Wishlist> ProvideWishlists(List<int> l1, List<int> l2)
    {
        /*
         * For each element from l1, randomly assigns priority for every element from l2
         */

        var l2Length = l2.Count;
        var result = new List<Wishlist>();

        foreach (var element in l1)
        {
            var l2Indexes = Enumerable.Range(0, l2Length).ToList();

            var currentElementPreferences = new int[l2Length];

            for (var i = 0; i < l2Length; i++)
            {
                var randomIndex = _random.Next(0, l2Indexes.Count);
                var tmp = l2Indexes.ElementAt(randomIndex);
                currentElementPreferences[i] = l2[tmp];
                l2Indexes.RemoveAt(randomIndex);
            }

            result.Add(new Wishlist(element, currentElementPreferences));
        }

        return result;
    }
}
