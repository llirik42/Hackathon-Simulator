namespace HackathonProblem.Db.exceptions;

public class JuniorAlreadyExistsException(int juniorId, Exception innerException)
    : Exception($"Junior with id {juniorId} already exists", innerException);

