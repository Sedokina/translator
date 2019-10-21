using System.Collections.Generic;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Interfaces
{
    public interface IWordMapper
    {
        IWord Find(long id);
        IWord Find(string text);
        IEnumerable<IWord> GetWords();
        IEnumerable<ITranslation> GetTranslations();
        IEnumerable<ITranslation> FindTranslation(string text, short languageId);
        IWord Add(IWord word);
        long AddTranslate(long translatableId, long translatedId);
        void UpdateTranslate(long translationId, long translatableId, long translatedId);
        void Delete(long id);
    }
}
