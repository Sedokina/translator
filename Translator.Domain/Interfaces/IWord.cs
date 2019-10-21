namespace Translator.Domain.Interfaces
{
    public interface IWord
    {
        long Id { get; }
        string Text { get; set; }
        ILanguage Language { get; set; }
    }
}
