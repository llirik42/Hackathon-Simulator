using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.entities;

namespace HackathonProblem.Db.services;

public class TeamLeadWishlistService(DbConfiguration configuration) : ITeamLeadWishlistService
{
    public void AddTeamLeadWishlist(int hackathonId, Wishlist teamLeadWishlist)
    {
        using var db = new ApplicationContext(configuration);
        var teamLeadId = teamLeadWishlist.EmployeeId;

        var entities = teamLeadWishlist.DesiredEmployees.Select((desiredJuniorId, i) => new TeamLeadPreferenceEntity
        {
            HackathonId = hackathonId,
            TeamLeadId = teamLeadId,
            DesiredJuniorId = desiredJuniorId,
            DesiredJuniorPriority = i
        }).ToList();

        db.TeamLeadPreferences.AddRange(entities);
        db.SaveChanges();
    }
}
