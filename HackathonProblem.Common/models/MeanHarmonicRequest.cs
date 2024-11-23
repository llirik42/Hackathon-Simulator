using System.ComponentModel.DataAnnotations;

namespace HackathonProblem.Common.models;

public class MeanHarmonicRequest
{
    [Required]
    public required double[] Numbers { get; set; }
}
