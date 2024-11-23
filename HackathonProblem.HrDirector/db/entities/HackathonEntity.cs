namespace HackathonProblem.HrDirector.db.entities;

public class HackathonEntity
{
    public int Id { get; init; }

    public DateTime CreationDate { get; init; }

    public required double Harmonization { get; init; }
}
