using Translator.Domain.Interfaces;

namespace Translator.Domain.Domains
{
    public class Word : IWord
    {
        public long Id { get; private set; }
        public string Text { get; set; }
        public ILanguage Language { get; set; }

        public Word(string text, ILanguage language)
        {
            Text = text;
            Language = language;
        }

        public Word(long id, string text, ILanguage language)
        {
            Id = id;
            Text = text;
            Language = language;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Text)}: {Text}, {nameof(Language)}: {Language}";
        }
    }
}
