using System.Collections.Generic;
using Translator.Domain.Interfaces;

namespace Translator.DataMapper.Interfaces
{
    public interface IWordMapper
    {
        IWord Find(long id);
        IWord Find(string text);
        IEnumerable<IWord> GetWords();
        IWord Add(IWord word);
    }
}
