using Hackathon_Simulator.dto;

namespace Hackathon_Simulator.services;

public interface IEmployeeProvider
{
    List<Employee> Provide(string filePath);
}
