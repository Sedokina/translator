using System.Collections.Generic;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Interfaces
{
    public interface IRoleMapper
    {
        IEnumerable<IRole> GetRoles();
        IEnumerable<IRole> GetUserRoles(int id);
    }
}
