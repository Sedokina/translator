namespace Translator.Domain
{
    public class Translation
    {
        public long Id { get; private set; }
        public Word Translatable { get; set; }
        public Word Translated { get; set; }

        public Translation(long id, Word translatable, Word translated)
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
