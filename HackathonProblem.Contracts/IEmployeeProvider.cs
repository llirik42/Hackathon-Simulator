namespace HackathonProblem.Contracts;

public interface IEmployeeProvider
{
    List<Employee> Provide(string url);
}
