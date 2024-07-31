using System.Net;

namespace Catalog.ExceptionManager.ExceptionBase;

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
