using System.Net;

namespace ExceptionManager.ExceptionBase;

public class InvalidValueException : BaseException
{
    public InvalidValueException(string message) : base(message) 
    {
    }

    public override IList<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }
}
