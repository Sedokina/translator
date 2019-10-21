using System.Collections.Generic;
using Translator.Domain;

namespace Translator.DataMapper.Interfaces
{
    public interface ILanguageMapper
    {
        Language Find(short id);
        IEnumerable<Language> GetLanguages();
    }
}
