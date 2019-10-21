namespace Translator.Domain.Interfaces
{
    public interface ITranslation
    {
        long Id { get; }
        IWord Translatable { get; set; }
        IWord Translated { get; set; }
    }
}
