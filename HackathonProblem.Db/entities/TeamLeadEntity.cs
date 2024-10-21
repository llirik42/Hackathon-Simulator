using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.Db.entities;

public class TeamLeadEntity
{
    public int Id { get; init; }

    [MaxLength(64)] public string Name { get; init; }
}
