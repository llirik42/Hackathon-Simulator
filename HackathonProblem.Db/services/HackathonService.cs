using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.entities;

namespace HackathonProblem.Db.services;

public class HackathonService(DbConfiguration configuration) : IHackathonService
{
    public int CreateHackathon(double harmonization)
    {
        using var db = new ApplicationContext(configuration);
        var hackathonEntity = new HackathonEntity { Harmonization = harmonization };
        db.Hackathons.Add(hackathonEntity);
        db.SaveChanges();
        return hackathonEntity.Id;
    }

    public Hackathon FindHackathon(int hackathonId)
    {
        throw new NotImplementedException();
    }

    public double GetAverageHarmonization()
    {
        using var db = new ApplicationContext(configuration);
        return db.Hackathons.Average(h => h.Harmonization);
    }
}
