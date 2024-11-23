using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.HrDirector.db.entities;

public class TeamLeadEntity
{
    public int Id { get; init; }

    [MaxLength(64)] public required string Name { get; init; }
}
