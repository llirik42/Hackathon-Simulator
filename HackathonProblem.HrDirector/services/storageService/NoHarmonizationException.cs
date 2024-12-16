namespace HackathonProblem.HrDirector.services.storageService;

public class NoHarmonizationException(int hackathonId, Exception? innerException = null)
    : Exception($"Hackathon {hackathonId} has not harmonization", innerException);
