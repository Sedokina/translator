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
    public class TranslationMapper : ITranslationMapper
    {
        private readonly IDbManager _dbManager;

        private static readonly string BaseRequest =
            @"SELECT t.id, t.translatableId, w.word as translatableWord, l.id as translatableLanguageId, " +
            "l.name as translatableLanguage, t.translatedId, w2.word as translatedWord, l2.id as translatedLanguageId, " +
            "l2.name as translatedLanguage FROM translator.translation as t" +
            " Left Join  translator.words as w on t.translatableId = w.id" +
            " Left Join  translator.words as w2 on t.translatedId = w2.id" +
            " Left Join languages as l on w.languageId = l.id" +
            " Left Join languages as l2 on w2.languageId = l2.id";

        private static readonly string FindByWordRequest =
            $"{BaseRequest} Where l2.id = @languageId and w.word LIKE concat('%', @text, '%')";

        private static readonly string InsertRequest =
            "INSERT INTO translation (translatableId, translatedId) Values (@translatableId, @translatedId)";

        private static readonly string UpdateRequest =
            "UPDATE translation SET translatableId = @translatableId, translatedId = @translatedId WHERE id = @translationId";

        private static readonly string DeleteRequest = "DELETE FROM translation WHERE id = @id";

        public TranslationMapper()
        {
            _dbManager = ServiceLocator.Instance.GetService<IDbManager>();
        }

        public IEnumerable<ITranslation> GetTranslations()
        {
            return ExecuteListQuery(BaseRequest);
        }

        public IEnumerable<ITranslation> FindTranslation(string text, short languageId)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@text", text, DbType.String),
                _dbManager.CreateParameter("@languageId", languageId, DbType.Int16)
            };
            return ExecuteListQuery(FindByWordRequest, parameters);
        }

        public long Add(long translatableId, long translatedId)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@translatableId", translatableId, DbType.Int64),
                _dbManager.CreateParameter("@translatedId", translatedId, DbType.Int64)
            };

            var id = _dbManager.Insert(InsertRequest, CommandType.Text, parameters);
            return Convert.ToInt64(id);
        }

        public void Update(long translationId, long translatableId, long translatedId)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@translationId", translationId, DbType.Int64),
                _dbManager.CreateParameter("@translatableId", translatableId, DbType.Int64),
                _dbManager.CreateParameter("@translatedId", translatedId, DbType.Int64)
            };

            _dbManager.Update(UpdateRequest, CommandType.Text, parameters);
        }

        public void Delete(long id)
        {
            var parameters = new[]
            {
                _dbManager.CreateParameter("@id", id, DbType.Int64),
            };

            _dbManager.Delete(DeleteRequest, CommandType.Text, parameters);
        }

        private IEnumerable<ITranslation> ExecuteListQuery(string request, IDbDataParameter[] parameters = null)
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
