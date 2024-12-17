using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.Common.models;

public class ShortTeam
{
    [Required] public int JuniorId { get; init; }

    [Required] public int TeamLeadId { get; init; }
}
