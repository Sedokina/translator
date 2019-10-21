using Translator.Presenters.Interfaces;
using Translator.Views;

namespace Translator.Presenters
{
    public abstract class BasePresenter<TView> : IPresenter where TView : IView
    {
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
