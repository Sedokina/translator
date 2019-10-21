using System.Collections.Generic;
using System.Linq;
using Translator.Domain.Interfaces;

namespace Translator.Domain.Domains
{
    public class User : IUser
    {
        private readonly string _password;

        public int Id { get; private set; }
        public string Username { get; set; }
        public List<IRole> Roles { get; private set; }
        public bool IsAuthorized { get; private set; }

        public User()
        {
            
        }

        public User(int id, string username)
        {
            Id = id;
            Username = username;
        }

        public User(int id, string username, string password)
        {
            _password = password;
            Id = id;
            Username = username;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Username)}: {Username}";
        }

        public bool IsInRole(string role)
        {
            return Roles.Any(x => x.Name.Equals(role));
        }
    }
}
