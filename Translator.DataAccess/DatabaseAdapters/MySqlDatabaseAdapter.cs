using System.Data;
using MySql.Data.MySqlClient;

namespace Translator.DataAccess.DatabaseAdapters
{
    public class MySqlDatabaseAdapter : IDatabaseAdapter
    {
        public string ConnectionString { get; protected set; }

        public MySqlDatabaseAdapter(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public void CloseConnection(IDbConnection connection)
        {
            connection.Close();
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new MySqlCommand(commandText, (MySqlConnection)connection)
            {
                CommandType = commandType
            };
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new MySqlDataAdapter((MySqlCommand)command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            var sqlCommand = (MySqlCommand)command;
            return sqlCommand.CreateParameter();
        }

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            return new MySqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            return new MySqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }
    }
}
