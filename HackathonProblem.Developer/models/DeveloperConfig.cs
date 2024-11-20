namespace HackathonProblem.Developer.models;

public record DeveloperConfig(
    int DeveloperId,
    DeveloperType DeveloperType,
    string JuniorsUrl,
    string TeamLeadsUrl);
