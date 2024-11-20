namespace HackathonProblem.Common.exceptions;

public class UnexpectedResponseException(string response, Exception? innerException = null)
    : Exception($"Unexpected response: {response}", innerException);
