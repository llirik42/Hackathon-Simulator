using HackathonProblem.Common.models;

namespace HackathonProblem.Developer.models;

public class DeveloperConfig
{
    public int Id { get; init; }
    
    public DeveloperType Type { get; init; }

    public required string JuniorsUrl { get; init; }
    
    public required string TeamLeadsUrl { get; init; }
}
