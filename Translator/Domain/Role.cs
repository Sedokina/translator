namespace Translator.Domain
{
    public class Role
    {
        public short Id { get; private set; }
        public string Name { get; set; }

        public Role(short id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}
