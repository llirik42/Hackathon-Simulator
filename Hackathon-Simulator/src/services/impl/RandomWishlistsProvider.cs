using Hackathon_Simulator.dto;

namespace Hackathon_Simulator.services.impl;

public class RandomWishlistsProvider(List<Employee> juniors, List<Employee> teamLeads) : IWishlistProvider
{
    private readonly Random _random = new(0);

    public IEnumerable<Wishlist> ProvideJuniorsWishlists()
    {
        return ProvideWishlists(juniors, teamLeads);
    }

    public IEnumerable<Wishlist> ProvideTeamLeadsWishlists()
    {
        return ProvideWishlists(teamLeads, juniors);
    }

    private List<Wishlist> ProvideWishlists(List<Employee> l1, List<Employee> l2)
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
                currentElementPreferences[i] = l2[tmp].Id;
                l2Indexes.RemoveAt(randomIndex);
            }

            result.Add(new Wishlist(element.Id, currentElementPreferences));
        }

        return result;
    }
}
