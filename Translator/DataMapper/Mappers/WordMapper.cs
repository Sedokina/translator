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

        public WordMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public IWord Find(long id)
        {
            var idParameter = _dbManager.CreateParameter("@Id", id, DbType.Int16);
            var reader = _dbManager.GetDataReader(
                "SELECT words.Id as wordId, words.word, " + 
                "languages.id as languageId, languages.Name FROM words" +
                " LEFT JOIN languages on words.languageId = languages.id" +
                " Where words.id = @Id LIMIT 1",
                CommandType.Text, new[] { idParameter }, out var connection);

            Word word = null;
            if (reader.Read())
            {
                word = new Word(reader.GetInt64(0), reader.GetString(1),
                    new Language(reader.GetInt16(2), reader.GetString(3)));
            }
            connection.Close();
            return word;
        }

        public IWord Find(string text)
        {
            var idParameter = _dbManager.CreateParameter("@text", text, DbType.String);
            var reader = _dbManager.GetDataReader(
                "SELECT words.Id as wordId, words.word, " +
                "languages.id as languageId, languages.Name FROM words" +
                " LEFT JOIN languages on words.languageId = languages.id" +
                " Where words.word LIKE concat('%', @text, '%') LIMIT 1",
                CommandType.Text, new[] { idParameter }, out var connection);

            Word word = null;
            if (reader.Read())
            {
                word = new Word(reader.GetInt64(0), reader.GetString(1),
                    new Language(reader.GetInt16(2), reader.GetString(3)));
            }
            connection.Close();
            return word;
        }

        public IEnumerable<IWord> GetWords()
        {
            var reader = _dbManager.GetDataReader(
                "SELECT words.Id as wordId, words.word, " +
                "languages.id as languageId, languages.Name FROM words" +
                " LEFT JOIN languages on words.languageId = languages.id",
                CommandType.Text, null, out var connection);

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


        private const string TranslationRequest =
            @"SELECT t.id, t.translatableId, w.word as translatableWord, l.id as translatableLanguageId, " +
            "l.name as translatableLanguage, t.translatedId, w2.word as translatedWord, l2.id as translatedLanguageId, " +
            "l2.name as translatedLanguage FROM translator.translation as t" +
            " Left Join  translator.words as w on t.translatableId = w.id" +
            " Left Join  translator.words as w2 on t.translatedId = w2.id" +
            " Left Join languages as l on w.languageId = l.id" +
            " Left Join languages as l2 on w2.languageId = l2.id";

        public IEnumerable<ITranslation> FindTranslation(string text, short languageId)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@text", text, DbType.String),
                _dbManager.CreateParameter("@languageId", languageId, DbType.Int16)
            };
            return GetTranslations(
                $"{TranslationRequest} Where l2.id = @languageId and w.word LIKE concat('%', @text, '%')", parameters);
        }

        public IWord Add(IWord word)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@languageId", word.Language.Id, DbType.Int16),
                _dbManager.CreateParameter("@text", word.Text, DbType.String)
            };

            var id = _dbManager.Insert(
                "INSERT INTO words (languageId, word) values (@languageId, @text)",
                CommandType.Text, parameters);
            return new Word(Convert.ToInt64(id), word.Text, word.Language);
        }

        public long AddTranslate(long translatableId, long translatedId)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@translatableId", translatableId, DbType.Int64),
                _dbManager.CreateParameter("@translatedId", translatedId, DbType.Int64)
            };

            var id = _dbManager.Insert(
                "INSERT INTO translation (translatableId, translatedId) Values (@translatableId, @translatedId)",
                CommandType.Text, parameters);
            return Convert.ToInt64(id);
        }

        public void UpdateTranslate(long translationId, long translatableId, long translatedId)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@translationId", translationId, DbType.Int64),
                _dbManager.CreateParameter("@translatableId", translatableId, DbType.Int64),
                _dbManager.CreateParameter("@translatedId", translatedId, DbType.Int64)
            };

            _dbManager.Update(
                "UPDATE translation SET translatableId = @translatableId, translatedId = @translatedId WHERE id = @translationId",
                CommandType.Text, parameters);
        }

        public void Delete(long id)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@id", id, DbType.Int64),
            };

            _dbManager.Delete(
                "DELETE FROM translation WHERE id = @id",
                CommandType.Text, parameters);
        }

        public IEnumerable<ITranslation> GetTranslations()
        {
            return GetTranslations(TranslationRequest);
        }

        private IEnumerable<ITranslation> GetTranslations(string request, IDbDataParameter[] parameters = null)
        {
            var reader = _dbManager.GetDataReader(request, CommandType.Text, parameters, out var connection);

            var translations = new List<Translation>();
            while (reader.Read())
            {
                var translation = new Translation(reader.GetInt64(0),
                    new Word(reader.GetInt64(1), reader.GetString(2), new Language(reader.GetInt16(3), reader.GetString(4))),
                    new Word(reader.GetInt64(5), reader.GetString(6), new Language(reader.GetInt16(7), reader.GetString(8))));
                translations.Add(translation);
            }
            connection.Close();
            return translations;
        }
    }
}
