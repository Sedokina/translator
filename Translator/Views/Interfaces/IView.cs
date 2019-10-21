namespace Translator.Views.Interfaces
{
    public interface IView
    {
        void Show();
        void Close();
        void ShowMessage(string text);
        void ShowError(string text);
    }
}
