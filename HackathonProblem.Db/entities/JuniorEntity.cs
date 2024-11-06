using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.Db.entities;

public class JuniorEntity
{
    public int Id { get; init; }

    [MaxLength(64)] public required string Name { get; init; }
}
