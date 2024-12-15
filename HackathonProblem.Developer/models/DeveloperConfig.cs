using HackathonProblem.Common.models;

namespace HackathonProblem.Developer.models;

public class DeveloperConfig
{
    public int Id { get; set; }
    
    public DeveloperType Type { get; set; }

    public string JuniorsUrl { get; set; }
    
    public string TeamLeadsUrl { get; set; }
}
