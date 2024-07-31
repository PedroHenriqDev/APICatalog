using System.Net;

namespace Catalog.ExceptionManager.ExceptionBase;

public class SettingsNotFoundException : BaseException
{
    public SettingsNotFoundException(string message) : base(message) 
    {
    }

    public override IList<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.InternalServerError;
    }
}
