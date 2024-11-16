namespace HackathonProblem.CsvEmployeeProvider;

public class CsvEmployeeProviderException(string message, Exception innerException)
    : Exception(message, innerException);
