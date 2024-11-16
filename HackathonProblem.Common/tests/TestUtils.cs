using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.tests;

public static class TestUtils
{
    public static void ShiftLeft<T>(List<T> list, int shift)
    {
        var copy = new List<T>(list);

        for (var i = shift; i < list.Count; i++) list[i - shift] = list[i];

        for (var i = list.Count - shift; i < list.Count; i++) list[i] = copy[i + shift - list.Count];
    }
    
    public static List<Employee> GetSimpleEmployees(int count)
    {
        var employees = new List<Employee>();

        for (var i = 0; i < count; i++)
        {
            var id = i + 1;
            employees.Add(GetSimpleEmployee(id));
        }

        return employees;
    }
    
    private static Employee GetSimpleEmployee(int id)
    {
        return new Employee(id, $"{id}");
    }
}
