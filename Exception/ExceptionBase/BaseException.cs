﻿using System.Net;

namespace ExceptionManager.ExceptionBase;

public abstract class BaseException : SystemException
{
    public BaseException(string message) : base(message)
    {
    }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}
