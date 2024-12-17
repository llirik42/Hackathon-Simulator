namespace HackathonProblem.HrDirector.models;

public class HrDirectorConfig
{
    public required string JuniorsUrl { get; init; }
    
    public required string TeamLeadsUrl { get; init; }
    
    public required int EmployeeCount { get; init; }
    
    public required int HackathonCount { get; init; }
}
