using Translator.Domain;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Interfaces
{
    public interface IUserMapper
    {
        IUser Find(string username);
        IUser FindWithPassword(string username);
    }
}
