using System.Data;

namespace Translator.DataAccess
{
    public interface IDatabaseAdapter
    {
        IDbConnection CreateConnection();
        void CloseConnection(IDbConnection connection);
        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);
        IDataAdapter CreateAdapter(IDbCommand command);
        IDbDataParameter CreateParameter(IDbCommand command);
        IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection direction);
        IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction);
    }
}
