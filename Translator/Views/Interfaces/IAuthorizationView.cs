using System;

namespace Translator.Views.Interfaces
{
    public interface IAuthorizationView : IView
    {
        string Username { get; set; }
        string Password { get; set; }
        event Action Authorize;
    }
}
