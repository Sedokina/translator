using System.Data;
using Translator.DataAccess;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain.Domains;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Mappers
{
    public class UserMapper : IUserMapper
    {
        private readonly IDbManager _dbManager;
        private static readonly string GetUserRequest = "SELECT id, username, password FROM users WHERE username = @username";

        public UserMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public IUser Find(string username)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@username", username, DbType.String)
            };
            return ExecuteObjectQuery(GetUserRequest, out string password, parameters);
        }

        public IUser FindWithPassword(string username, out string password)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@username", username, DbType.String)
            };
            return ExecuteObjectQuery(GetUserRequest, out password, parameters);
        }

        public IUser ExecuteObjectQuery(string request, out string password, IDbDataParameter[] parameters = null)
        {
            var reader = _dbManager.GetDataReader(request, CommandType.Text, parameters, out var connection);

            User user = null;
            password = string.Empty;

            if (reader.Read())
            {
                user = new User(reader.GetInt32(0), reader.GetString(1));
                password = reader.GetString(2);
            }
            connection.Close();
            return user;
        }
    }
}
