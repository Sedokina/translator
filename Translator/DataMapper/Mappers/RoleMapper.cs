using System.Collections.Generic;
using System.Data;
using Translator.DataAccess;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain.Domains;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Mappers
{
    public class RoleMapper : IRoleMapper
    {
        private readonly IDbManager _dbManager;
        private static readonly string BaseRequest = "SELECT roles.id, roles.name FROM roles";
        private static readonly string UserRolesRequest =
            $"{BaseRequest} LEFT JOIN user_roles ON roles.id = user_roles.roleId Where user_roles.userId = @id";

        public RoleMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public IEnumerable<IRole> GetRoles()
        {
            return ExecuteListQuery(BaseRequest);
        }

        public IEnumerable<IRole> GetUserRoles(int id)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@id", id, DbType.Int32)
            };
            return ExecuteListQuery(UserRolesRequest, parameters);
        }

        private IEnumerable<IRole> ExecuteListQuery(string request, IDbDataParameter[] parameters = null)
        {
            var reader = _dbManager.GetDataReader(request, CommandType.Text, parameters, out var connection);

            var roles = new List<Role>();
            while (reader.Read())
            {
                var role = new Role(reader.GetInt16(0), reader.GetString(1));
                roles.Add(role);
            }
            connection.Close();
            return roles;
        }
    }
}
