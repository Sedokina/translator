using Translator.Domain;

namespace Translator.DataMapper.Interfaces
{
    public interface IUserMapper
    {
        User Find(string username);
        User FindWithPassword(string username);
    }
}
