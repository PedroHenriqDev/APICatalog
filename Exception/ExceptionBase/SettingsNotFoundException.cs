﻿using System.Net;

namespace ExceptionManager.ExceptionBase;

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
