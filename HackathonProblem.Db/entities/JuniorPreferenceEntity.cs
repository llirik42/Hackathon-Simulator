namespace HackathonProblem.Db.entities;

public class JuniorPreferenceEntity
{
    public int HackathonId { get; init; }
    public HackathonEntity? Hackathon { get; init; }

    public required int JuniorId { get; init; }
    public JuniorEntity? Junior { get; init; }

    public required int DesiredTeamLeadId { get; init; }
    public TeamLeadEntity? DesiredTeamLead { get; init; }

    public required int DesiredTeamLeadPriority { get; init; }
}
