using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.HrManager.models;

public class JuniorWishlistRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int JuniorId { get; init; }
    
    [Required]
    public int[] DesiredTeamLeads { get; init; }
}
