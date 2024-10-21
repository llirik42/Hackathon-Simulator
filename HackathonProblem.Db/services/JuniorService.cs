using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.entities;

namespace HackathonProblem.Db.services;

public class JuniorService(DbConfiguration configuration) : IJuniorService
{
    public int CreateJunior(string name)
    {
        using var db = new ApplicationContext(configuration);
        var juniorEntity = new JuniorEntity { Name = name };
        db.Juniors.Add(juniorEntity);
        db.SaveChanges();
        return juniorEntity.Id;
    }

    public void CreateJunior(int id, string name)
    {
        using var db = new ApplicationContext(configuration);
        var juniorEntity = new JuniorEntity { Id = id, Name = name };
        db.Juniors.Add(juniorEntity);
        db.SaveChanges();
    }

    public Employee? FindJunior(int id)
    {
        using var db = new ApplicationContext(configuration);
        var juniorEntity = db.Juniors.Find(id);
        return juniorEntity == null ? null : new Employee(juniorEntity.Id, juniorEntity.Name);
    }
}
