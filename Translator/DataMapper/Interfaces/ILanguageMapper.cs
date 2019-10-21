using System.Collections.Generic;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Interfaces
{
    public interface ILanguageMapper
    {
        ILanguage Find(short id);
        IEnumerable<ILanguage> GetLanguages();
    }
}
