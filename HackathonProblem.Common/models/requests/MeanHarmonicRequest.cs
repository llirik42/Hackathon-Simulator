using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.Common.models.requests;

public class MeanHarmonicRequest
{
    [Required] public required double[] Numbers { get; init; }
}
