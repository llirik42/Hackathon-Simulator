using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.Common.models;

public class ShortTeam
{
    [Required] public int JuniorId { get; set; }

    [Required] public int TeamLeadId { get; set; }
}
