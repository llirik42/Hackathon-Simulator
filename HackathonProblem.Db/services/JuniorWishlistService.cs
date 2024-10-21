using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.entities;

namespace HackathonProblem.Db.services;

public class JuniorWishlistService(DbConfiguration configuration) : IJuniorWithlistService
{
    public void AddJuniorWishlist(int hackathonId, Wishlist juniorWishlist)
    {
        using var db = new ApplicationContext(configuration);
        var juniorId = juniorWishlist.EmployeeId;

        var entities = juniorWishlist.DesiredEmployees.Select((desiredTeamLeadId, i) => new JuniorPreferenceEntity
        {
            HackathonId = hackathonId,
            JuniorId = juniorId,
            DesiredTeamLeadId = desiredTeamLeadId,
            DesiredTeamLeadPriority = i
        }).ToList();

        db.JuniorPreferences.AddRange(entities);
        db.SaveChanges();
    }
}
