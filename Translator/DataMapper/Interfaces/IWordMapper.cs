using System.Collections.Generic;
using Translator.Domain;

namespace Translator.DataMapper.Interfaces
{
    public interface IWordMapper
    {
        Word Find(long id);
        Word Find(string text);
        IEnumerable<Word> GetWords();
        IEnumerable<Translation> GetTranslations();
        IEnumerable<Translation> FindTranslation(string text, short languageId);
        Word Add(Word word);
        long AddTranslate(long translatableId, long translatedId);
        void UpdateTranslate(long translationId, long translatableId, long translatedId);
        void Delete(long id);
    }
}
