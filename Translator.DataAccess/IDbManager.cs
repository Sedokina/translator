using System.Data;

namespace Translator.DataAccess
{
    public interface IDbManager
    {
        IDbConnection CreateDatabaseConnection();
        void CloseConnection(IDbConnection connection);

        IDbDataParameter CreateParameter(string name, object value, DbType dbType,
            ParameterDirection parameterDirection = ParameterDirection.Input);

        IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType,
            ParameterDirection parameterDirection = ParameterDirection.Input);

        IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters,
            out IDbConnection connection);

        object Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters);
        void Update(string commandText, CommandType commandType, IDbDataParameter[] parameters);
        void Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters);
    }
}
