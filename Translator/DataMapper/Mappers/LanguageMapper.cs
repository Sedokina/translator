using System.Collections.Generic;
using System.Data;
using Translator.DataAccess;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain;

namespace Translator.DataMapper.Mappers
{
    public class LanguageMapper : ILanguageMapper
    {
        private readonly IDbManager _dbManager;

        public LanguageMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public Language Find(short id)
        {
            var idParameter =_dbManager.CreateParameter("@Id", id, DbType.Int16);
            var reader = _dbManager.GetDataReader("SELECT * FROM languages Where id = @Id LIMIT 1",
                CommandType.Text, new []{ idParameter }, out var connection);

            Language language = null;
            if(reader.Read())   
            {
                language = new Language(reader.GetInt16(0), reader.GetString(1));
            }
            connection.Close();
            return language;
        }

        public IEnumerable<Language> GetLanguages()
        {
            var reader = _dbManager.GetDataReader("SELECT * FROM languages",
                CommandType.Text, null, out var connection);

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
