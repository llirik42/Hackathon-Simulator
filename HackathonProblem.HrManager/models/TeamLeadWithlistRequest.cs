using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.HrManager.models;

public class TeamLeadWithlistRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int TeamLeadId { get; init; }
    
    [Required]
    public int[] DesiredJuniors { get; init; }
}
