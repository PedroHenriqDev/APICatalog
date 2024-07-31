namespace Catalog.Application.Extensions;

public static class ErrorMessageExtension
{
    public static string FormatErrorMessage<T>(this string message, T invalidValue)
    {
        return string.Format(message, invalidValue);
    }
}
