using Catalog.Application.Extensions;
using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;

namespace Catalog.Application.Validations;

public static class EntityValidatorHelper
{
    public static void ValidId(int id)
    {
        if (id <= 0)
        {
            var errorMessage = ErrorMessagesResource.INVALID_ID.FormatErrorMessage(id);
            throw CreateException<InvalidValueException>(errorMessage);
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
        if (!items.Any())
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
        var constructor = typeof(TException).GetConstructor(new[] { typeof(string) });

        if (constructor is null)
        {
            throw new InvalidOperationException(ErrorMessagesResource.INCOMPATIVE_BUILDERS.FormatErrorMessage(typeof(TException).Name));
        }

        throw (TException)constructor.Invoke(new object[] { message });
    }
}
