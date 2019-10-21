using Translator.Dependencies;
using Translator.Presenters.Interfaces;
using Translator.Services.Interfaces;
using Translator.Views.Interfaces;

namespace Translator.Presenters
{
    public abstract class BasePresenter<TView> : IPresenter where TView : IView
    {
        protected readonly ICredentialsService Credentials = ServiceLocator.Instance.GetService<ICredentialsService>();
        protected TView View { get; set; }

        protected BasePresenter()
        {
            
        }

        protected BasePresenter(TView view)
        {
            View = view;
        }

        public void Run()
        {
            View.Show();
        }
    }
}
