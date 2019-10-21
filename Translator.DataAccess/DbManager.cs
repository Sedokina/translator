using System.Configuration;
using System.Data;
using Translator.DataAccess.DatabaseAdapters;

namespace Translator.DataAccess
{
    public class DbManager : IDbManager
    {
        private readonly IDatabaseAdapter _database;

        public DbManager()
        {
            var connection = ConfigurationManager.ConnectionStrings["TranslateConnection"].ConnectionString;
            _database = new MySqlDatabaseAdapter(connection);
        }

        public IDbConnection CreateDatabaseConnection()
        {
            return _database.CreateConnection();
        }

        public void CloseConnection(IDbConnection connection)
        {
            _database.CloseConnection(connection);
        }

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            return _database.CreateParameter(name, value, dbType, parameterDirection);
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            return _database.CreateParameter(name, size, value, dbType, parameterDirection);
        }

        public IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters,
            out IDbConnection connection)
        {
            connection = _database.CreateConnection();
            connection.Open();

            var command = _database.CreateCommand(commandText, commandType, connection);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            var reader = command.ExecuteReader();
            return reader;
        }

        public void Update(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = _database.CreateConnection())
            {
                connection.Open();
                using (var command = _database.CreateCommand(commandText, commandType, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public object Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = _database.CreateConnection())
            {
                connection.Open();
                using (var command = _database.CreateCommand(commandText, commandType, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT LAST_INSERT_ID()";
                    var reader = command.ExecuteReader();
                    if (reader != null && reader.Read())
                    {
                        return reader.GetValue(0);
                    }
                }
            }

            return 0;
        }

        public void Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = _database.CreateConnection())
            {
                connection.Open();
                using (var command = _database.CreateCommand(commandText, commandType, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
