namespace HackathonProblem.Db.entities;

public class JuniorPreferenceEntity
{
    public int HackathonId { get; init; }
    public HackathonEntity Hackathon { get; init; }

    public int JuniorId { get; init; }
    public JuniorEntity Junior { get; init; }

    public int DesiredTeamLeadId { get; init; }
    public TeamLeadEntity DesiredTeamLead { get; init; }

    public int DesiredTeamLeadPriority { get; init; }
}
