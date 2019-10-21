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
    public class WordMapper : IWordMapper
    {

        private readonly IDbManager _dbManager;

        private static readonly string BaseRequest =
            "SELECT words.Id as wordId, words.word, " +
            "languages.id as languageId, languages.Name FROM words" +
            " LEFT JOIN languages on words.languageId = languages.id";

        private static readonly string GetByIdRequest =
            $"{BaseRequest} Where words.id = @Id LIMIT 1";

        private static readonly string GetByTextRequest =
            $"{BaseRequest} Where words.word LIKE concat('%', @text, '%') LIMIT 1";

        private static readonly string InsertRequest =
            "INSERT INTO words (languageId, word) values (@languageId, @text)";

        public WordMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public IWord Find(long id)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@Id", id, DbType.Int16)
            };
            return ExecuteObjectRequest(GetByIdRequest, parameters);
        }

        public IWord Find(string text)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@text", text, DbType.String)
            };
            return ExecuteObjectRequest(GetByTextRequest, parameters);
        }

        public IEnumerable<IWord> GetWords()
        {
            return ExecuteListRequest(BaseRequest);
        }

        public IWord Add(IWord word)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@languageId", word.Language.Id, DbType.Int16),
                _dbManager.CreateParameter("@text", word.Text, DbType.String)
            };

            var id = _dbManager.Insert(InsertRequest, CommandType.Text, parameters);
            return new Word(Convert.ToInt64(id), word.Text, word.Language);
        }

        private IWord ExecuteObjectRequest(string request, IDbDataParameter[] parameters = null)
        {
            var reader = _dbManager.GetDataReader(request, CommandType.Text, parameters, out var connection);

            Word word = null;
            if (reader.Read())
            {
                word = new Word(reader.GetInt64(0), reader.GetString(1),
                    new Language(reader.GetInt16(2), reader.GetString(3)));
            }
            connection.Close();
            return word;
        }

        private IEnumerable<IWord> ExecuteListRequest(string request, IDbDataParameter[] parameters = null)
        {
            var reader = _dbManager.GetDataReader(request, CommandType.Text, parameters, out var connection);

            var words = new List<Word>();
            while (reader.Read())
            {
                var word = new Word(reader.GetInt64(0), reader.GetString(1),
                    new Language(reader.GetInt16(2), reader.GetString(3)));
                words.Add(word);
            }
            connection.Close();
            return words;
        }
       
    }
}
