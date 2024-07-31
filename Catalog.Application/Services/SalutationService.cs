using Catalog.Application.Interfaces;

namespace Catalog.Application.Services;

public class SalutationService : ISalutationService
{
    public string Salutation(string name) 
    {
        if(name == null) throw new ArgumentNullException("Name is null!");
        return $"Salutation {name}, now is {DateTime.UtcNow}";
    }
}
