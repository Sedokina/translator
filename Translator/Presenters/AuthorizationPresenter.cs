using Translator.Dependencies;
using Translator.Resources;
using Translator.Views.Interfaces;

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
            if (Credentials.Authorize(username, password))
            {
                new TranslatorPresenter().Run();
                View.Close();
            }
            else
            {
                View.ShowError(MainResources.WrongCredentials);
            }
        }
    }
}
