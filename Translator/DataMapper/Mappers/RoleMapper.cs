using System;
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

        public RoleMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public IEnumerable<IRole> GetRoles()
        {
           var reader = _dbManager.GetDataReader(
                "SELECT * FROM roles",
                CommandType.Text, null, out var connection);

           var roles = new List<Role>();
           while (reader.Read())
           {
               var role = new Role(reader.GetInt16(0), reader.GetString(1));
               roles.Add(role);
           }
           connection.Close();
           return roles;
        }

        public IEnumerable<IRole> GetUserRoles(int id)
        {
            var idParameter = _dbManager.CreateParameter("@id", id, DbType.Int32);
            var reader = _dbManager.GetDataReader(
                "SELECT roles.id, roles.name FROM roles" +
                " LEFT JOIN user_roles ON roles.id = user_roles.roleId" +
                " Where user_roles.userId = @id",
                CommandType.Text, new []{idParameter}, out var connection);

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
