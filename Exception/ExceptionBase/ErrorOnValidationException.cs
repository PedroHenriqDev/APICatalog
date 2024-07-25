using System.Net;

namespace ExceptionManager.ExceptionBase;

public class ErrorOnValidationException : BaseException
{
    private readonly IList<string> _errorMessages;

    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public override IList<string> GetErrorMessages()
    {
        return _errorMessages;
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }
}
