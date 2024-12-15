using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.Common.models.requests;

public class EmployeeHarmonizationRequest
{
    [Required] public required int[] DesiredEmployees { get; set; }

    [Required] public required int DesiredEmployeeId { get; set; }
}
