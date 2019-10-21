namespace Translator.Domain
{
    public class Language
    {

        public short Id { get; private set; }
        public string Name { get; set; }

        public Language()
        {
        }

        public Language(short id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }
}
