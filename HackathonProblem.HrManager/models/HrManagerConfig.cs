namespace HackathonProblem.HrManager.models;

public record HrManagerConfig(
    string JuniorsUrl,
    string TeamLeadsUrl,
    int EmployeeCount,
    string HrDirectorConnectionString);
