using HackathonProblem.Contracts;

namespace HackathonProblem.HrManager;

public class HrManager(IHarmonizationCalculator harmonizationCalculator) : ITeamBuildingStrategy
{
    public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        var teamLeadsList = teamLeads.ToList();
        var juniorsList = juniors.ToList();
        var teamLeadsWishlistsList = teamLeadsWishlists.ToList();
        var juniorsWishlistsList = juniorsWishlists.ToList();

        // teamLeadsMap[id of team-lead] = team-lead
        var teamLeadsMap = teamLeadsList.ToDictionary(e => e.Id, e => e);

        // teamLeadsMap[id of junior] = junior
        var juniorsMap = juniorsList.ToDictionary(e => e.Id, e => e);

        // Team-leads send offers to juniors
        var choices1 = BuildChoices(teamLeadsList, teamLeadsWishlistsList, juniorsWishlistsList);

        // Juniors send offers to team-leads
        var choices2 = BuildChoices(juniorsList, juniorsWishlistsList, teamLeadsWishlistsList);

        // Getting list of teams from choices-1
        var result1 = new List<Team>();
        foreach (var (juniorId, teamLeadIndex) in choices1)
        {
            var junior = juniorsMap[juniorId];
            var teamLead = teamLeadsList[teamLeadIndex];
            result1.Add(new Team(teamLead, junior));
        }

        // Getting list of teams from choices-2
        var result2 = new List<Team>();
        foreach (var (teamLeadId, juniorIndex) in choices2)
        {
            var junior = juniorsList[juniorIndex];
            var teamLead = teamLeadsMap[teamLeadId];
            result2.Add(new Team(teamLead, junior));
        }

        var harmonization1 = harmonizationCalculator.CalculateTeamsHarmonization(result1, teamLeadsWishlistsList, juniorsWishlistsList);
        var harmonization2 = harmonizationCalculator.CalculateTeamsHarmonization(result2, teamLeadsWishlistsList, juniorsWishlistsList);

        return harmonization1 > harmonization2 ? result1 : result2;
    }

    private Dictionary<int, int> BuildChoices(IEnumerable<Employee> senders,
        IEnumerable<Wishlist> sendersWishlists, IEnumerable<Wishlist> receiversWishlists)
    {
        /*
         * https://en.wikipedia.org/wiki/Stable_marriage_problem
         */

        var sendersList = senders.ToList();

        // Count of senders and receivers (they must be equal)
        var count = sendersList.Count;

        // sendersWishListsMap[sender id] = desired receivers
        var sendersWishListsMap = sendersWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);

        // receiversWishListsMap[receiver id] = desired senders
        var receiversWishListsMap = receiversWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);

        // choices[id of receiver] = index of sender
        var choices = new Dictionary<int, int>();

        // Indexes of senders whose offers were approved by receivers earlier
        var chosenSenders = new List<int>();

        // offers[index of sender] = id of receiver
        var offers = new int[count];

        // Offsets lists of desired receivers of senders: offsets[index of sender] = offset for sender
        // Array initialized with all values equal to 0
        var sendersOffsets = new int[count];

        // areOffersPossible = true <=> there are senders that can send offers 
        var areOffersPossible = true;

        // Algorithm
        while (areOffersPossible)
        {
            areOffersPossible = false;

            // Senders send offers to receivers
            for (var senderIndex = 0; senderIndex < count; senderIndex++)
            {
                // Whether some receiver previously chose current sender
                if (chosenSenders.Contains(senderIndex)) continue;

                // Send offer to most desirable receiver with offset
                var sender = sendersList[senderIndex];
                var desiredReceivers = sendersWishListsMap[sender.Id];
                offers[senderIndex] = desiredReceivers[sendersOffsets[senderIndex]++];
                areOffersPossible |= sendersOffsets[senderIndex] != count;
            }

            // Receivers receive and consider offers
            for (var senderIndex = 0; senderIndex < count; senderIndex++)
            {
                var receiverId = offers[senderIndex];

                // Current receiver didn't have anybody chosen. Then current receiver chooses current sender
                if (choices.TryAdd(receiverId, senderIndex))
                {
                    // Sender is chosen by current receiver, so sender is added to list of chosen ones
                    chosenSenders.Add(senderIndex);
                    continue;
                }

                var chosenSenderIndex = choices[receiverId];
                var potentialSenderIndex = senderIndex;

                var chosenSenderId = sendersList[chosenSenderIndex].Id;
                var potentialSenderId = sendersList[potentialSenderIndex].Id;

                var desiredSenders = receiversWishListsMap[receiverId];

                var currentHarmonization =
                    harmonizationCalculator.CalculateEmployeeHarmonization(desiredSenders, chosenSenderId);
                var potentialHarmonization =
                    harmonizationCalculator.CalculateEmployeeHarmonization(desiredSenders, potentialSenderId);

                if (potentialHarmonization <= currentHarmonization)
                {
                    continue;
                }

                // Forget previously chosen sender and choose new one
                choices[receiverId] = potentialSenderIndex;
                chosenSenders.Remove(chosenSenderIndex);
                chosenSenders.Add(potentialSenderIndex);
            }
        }

        return choices;
    }
}
