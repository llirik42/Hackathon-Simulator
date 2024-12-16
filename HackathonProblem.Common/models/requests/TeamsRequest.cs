using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.Common.models.requests;

public class TeamsRequest
{
    [Required] public required List<ShortTeam> Teams { get; set; }
    
    [Required] public required int HackathonId { get; set; }
}
