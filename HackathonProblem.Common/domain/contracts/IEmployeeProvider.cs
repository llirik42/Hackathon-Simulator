using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.domain.contracts;

public interface IEmployeeProvider
{
    List<Employee> Provide(string url);
}
