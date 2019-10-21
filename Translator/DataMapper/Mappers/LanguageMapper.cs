using System.Collections.Generic;
using System.Data;
using System.Linq;
using Translator.DataAccess;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain.Domains;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Mappers
{
    public class LanguageMapper : ILanguageMapper
    {
        private readonly IDbManager _dbManager;
        private static readonly string BaseRequest = "SELECT * FROM languages";
        private static readonly string FindRequest = $"{BaseRequest} WHERE id = @Id LIMIT 1";

        public LanguageMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public ILanguage Find(short id)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@Id", id, DbType.Int16)
            };
            return ExecuteListQuery(FindRequest, parameters).FirstOrDefault();
        }

        public IEnumerable<ILanguage> GetLanguages()
        {
            return ExecuteListQuery(BaseRequest);
        }

        private IEnumerable<ILanguage> ExecuteListQuery(string request, IDbDataParameter[] parameters = null)
        {
            var reader = _dbManager.GetDataReader(request, CommandType.Text, parameters, out var connection);

            var languages = new List<Language>();
            while (reader.Read())
            {
                var language = new Language(reader.GetInt16(0), reader.GetString(1));
                languages.Add(language);
            }
            connection.Close();
            return languages;
        }
    }
}
