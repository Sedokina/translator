using System.Collections.Generic;
using Translator.Domain;

namespace Translator.DataMapper.Interfaces
{
    public interface IRoleMapper
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<Role> GetUserRoles(int id);
    }
}
