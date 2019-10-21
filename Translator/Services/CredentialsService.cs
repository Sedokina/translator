using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain.Domains;
using Translator.Domain.Interfaces;
using Translator.Services.Interfaces;

namespace Translator.Services
{
    public class CredentialsService : ICredentialsService, IUser
    {
        private readonly IUserMapper _userMapper = ServiceLocator.Instance.GetService<IUserMapper>();
        private readonly IRoleMapper _roleMapper = ServiceLocator.Instance.GetService<IRoleMapper>();

        public int Id { get; private set; }
        public string Username { get; set; }
        public List<IRole> Roles { get; private set; }
        public bool IsAuthorized { get; private set; }

        public bool Authorize(string username, string password)
        {
            var user = _userMapper.FindWithPassword(username, out string actualPassword);
            if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(password, actualPassword, HashType.SHA256))
                return IsAuthorized = false;

            Id = user.Id;
            Username = user.Username;

            var roles = _roleMapper.GetUserRoles(Id);
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
