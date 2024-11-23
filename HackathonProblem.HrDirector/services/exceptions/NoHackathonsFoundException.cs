namespace HackathonProblem.HrDirector.services.exceptions;

public class NoHackathonsFoundException(Exception? innerException = null) : Exception("No hackathons found", innerException);
