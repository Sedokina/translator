using System.Collections.Generic;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Interfaces
{
    public interface ITranslationMapper
    {
        IEnumerable<ITranslation> GetTranslations();
        IEnumerable<ITranslation> FindTranslation(string text, short languageId);
        long Add(long translatableId, long translatedId);
        void Update(long translationId, long translatableId, long translatedId);
        void Delete(long id);
    }
}
