using System.Collections.Generic;

namespace Translator.Domain.Interfaces
{
    public interface IUser
    {
        int Id { get; }
        string Username { get; set; }
        List<Role> Roles { get; }
        bool IsAuthorized { get; }
    }
}
