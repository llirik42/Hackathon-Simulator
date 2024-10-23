namespace HackathonProblem.Db.exceptions;

public class TeamLeadAlreadyExistsException(int teamLeadId, Exception innerException)
    : Exception($"Team-lead with id {teamLeadId} already exists", innerException);
