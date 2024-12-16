namespace HackathonProblem.HrDirector.services.storageService;

public class HackathonNotFoundException(int hackathonId, Exception? innerException = null)
    : Exception($"Hackathon {hackathonId} not found", innerException);
