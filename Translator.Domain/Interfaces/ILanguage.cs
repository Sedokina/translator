namespace Translator.Domain.Interfaces
{
    public interface ILanguage
    {
        short Id { get; }
        string Name { get; set; }
    }
}
