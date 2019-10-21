using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain.Interfaces;

namespace Translator.Domain
{
    public class User : IUser
    {
        private readonly IUserMapper _userMapper = ServiceLocator.Instance.GetService<IUserMapper>();
        private readonly IRoleMapper _roleMapper = ServiceLocator.Instance.GetService<IRoleMapper>();
        private readonly string _password;

        public int Id { get; private set; }
        public string Username { get; set; }
        public List<Role> Roles { get; private set; }
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

        public bool Authorize(string username, string password)
        {
            var user = _userMapper.FindWithPassword(username) as User;
            if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(password, user._password, HashType.SHA256))
                return IsAuthorized = false;

            Id = user.Id;
            Username = user.Username;

            var roles = _roleMapper.GetUserRoles(Id) as List<Role>;
            if (roles == null)
                return IsAuthorized = false;
            Roles = roles.ToList();

            ServiceLocator.Instance.AddInstantiatedService(typeof(User), this);
            return IsAuthorized = true;
        }

        public bool IsInRole(string role)
        {
            return Roles.Any(x => x.Name.Equals(role));
        }
    }
}
