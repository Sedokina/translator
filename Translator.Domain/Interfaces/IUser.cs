using System.Collections.Generic;
using Translator.Domain.Domains;

namespace Translator.Domain.Interfaces
{
    public interface IUser
    {
        int Id { get; }
        string Username { get; set; }
        List<IRole> Roles { get; }
        bool IsAuthorized { get; }
    }
}
