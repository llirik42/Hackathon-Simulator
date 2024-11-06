namespace HackathonProblem.Db.exceptions;

public class NoHackathonsFoundException(Exception? innerException = null) : Exception("No hackathons found", innerException);
