namespace HackathonProblem.Db.entities;

public class JuniorPreferenceEntity
{
    public int HackathonId { get; init; }
    public HackathonEntity HackathonEntity { get; init; }

    public int JuniorId { get; init; }
    public JuniorEntity JuniorEntity { get; init; }

    public int DesiredTeamLeadId { get; init; }
    public TeamLeadEntity DesiredTeamLeadEntity { get; init; }

    public int DesiredTeamLeadPriority { get; init; }
}
