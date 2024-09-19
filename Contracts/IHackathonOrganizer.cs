namespace Contracts;

public interface IHackathonOrganizer
{
    public HackathonMembers Organize(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors);
}
