using Translator.Dependencies;
using Translator.Domain;
using Translator.Views;

namespace Translator.Presenters
{
    public class AuthorizationPresenter : BasePresenter<IAuthorizationView>
    {
        public AuthorizationPresenter()
        {
            View = ServiceLocator.Instance.GetService<IAuthorizationView>();
            View.Authorize += () => Authorize(View.Username, View.Password);
        }

        public void Authorize(string username, string password)
        {
            var user = new User();
            if (user.Authorize(username, password))
            {
                new TranslatorPresenter().Run();
                View.Close();
            }
            else
            {
                View.ShowError("Неверный логин или пароль");
            }
        }
    }
}
