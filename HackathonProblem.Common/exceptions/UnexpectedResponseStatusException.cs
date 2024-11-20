namespace HackathonProblem.Common.exceptions;

public class UnexpectedResponseStatusException(string status, Exception? innerException = null)
    : Exception($"Unexpected status: {status}", innerException);
