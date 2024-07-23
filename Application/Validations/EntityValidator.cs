using Application.Extensions;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using System.Net.Http.Headers;

namespace Application.Validators;

public static class EntityValidator
{
    public static void ValidId<TException>(int id, string possibleErrorMessage)
        where TException : BaseException
    {
        if(id <= 0) 
        {
            throw CreateException<TException>(possibleErrorMessage);
        }
    }

    public static void ValidateNotNull<T, TException>(T? entity, string possibleErrorMessage) 
        where TException : BaseException
    {
        if (entity is null) 
        {
            throw CreateException<TException>(possibleErrorMessage);
        }
    }

    public static void ValidateEnumerableNotEmpty<T, TException>(IEnumerable<T?> items, string possibleErrorMessage)
        where TException : BaseException
    {
        if(!items.Any()) 
        {
            throw CreateException<TException>(possibleErrorMessage);
        }
    }

    public static void ValidateText<TException>(string text, string possibleErrorMessage) 
        where TException : BaseException
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            CreateException<TException>(possibleErrorMessage);
        } 
    }

    private static TException CreateException<TException>(string message) 
        where TException : BaseException
    {
        var constructor = typeof(TException).GetConstructor(new[] {typeof(string)});

        if (constructor is null)
        {
            throw new InvalidOperationException(ErrorMessagesResource.INCOMPATIVE_BUILDERS.FormatErrorMessage(typeof(TException).Name));
        }

        throw (TException)constructor.Invoke(new object[] { message });
    }
 }
