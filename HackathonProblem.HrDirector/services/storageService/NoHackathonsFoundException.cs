namespace HackathonProblem.HrDirector.services.storageService;

public class NoHackathonsFoundException(Exception? innerException = null)
    : Exception("No hackathons with harmonization found", innerException);
