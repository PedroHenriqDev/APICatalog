namespace ExceptionManager.ExceptionBase;

public class SettingsNotFoundException : SystemException
{
    public SettingsNotFoundException(string message) : base(message) 
    {
    }
}
