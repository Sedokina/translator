using System;
using System.Data;
using Translator.DataAccess;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Mappers
{
    public class UserMapper : IUserMapper
    {
        private readonly IDbManager _dbManager;

        public UserMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public IUser Find(string username)
        {
            var idParameter = _dbManager.CreateParameter("@username", username, DbType.Int32);
            var reader = _dbManager.GetDataReader(
                "SELECT id, username FROM users WHERE username = @username",
                CommandType.Text, new[] { idParameter }, out var connection);

            User user = null;
            if (reader.Read())
            {
                user = new User(reader.GetInt32(0), reader.GetString(1));
            }
            connection.Close();
            return user;
        }

        public IUser FindWithPassword(string username)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@username", username, DbType.String)
            };
            var reader = _dbManager.GetDataReader(
                "SELECT id, username, password FROM users WHERE username = @username",
                CommandType.Text, parameters, out var connection);

            User user = null;
            if (reader.Read())
            {
                user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }
            connection.Close();
            return user;
        }
    }
}
