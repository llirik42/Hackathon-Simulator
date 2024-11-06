using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IEmployeeProvider
{
    List<Employee> Provide(string url);
}
