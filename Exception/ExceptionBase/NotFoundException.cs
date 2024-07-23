using System.Net;

namespace ExceptionManager.ExceptionBase;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message) 
    {
    }

    public override IList<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.NotFound;
    }
}
