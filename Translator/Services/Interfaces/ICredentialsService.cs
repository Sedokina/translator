using Translator.Domain.Interfaces;

namespace Translator.Services.Interfaces
{
    public interface ICredentialsService
    {
        bool Authorize(string name, string password);
        bool IsInRole(string role);
    }
}
