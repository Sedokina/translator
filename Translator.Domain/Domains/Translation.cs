using Translator.Domain.Interfaces;

namespace Translator.Domain.Domains
{
    public class Translation : ITranslation
    {
        public long Id { get; private set; }
        public IWord Translatable { get; set; }
        public IWord Translated { get; set; }

        public Translation(long id, IWord translatable, IWord translated)
        {
            Id = id;
            Translatable = translatable;
            Translated = translated;
        }

        public override string ToString()
        {
            return $"{nameof(Translatable)}: {Translatable}, {nameof(Translated)}: {Translated}";
        }
    }
}
