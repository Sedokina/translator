using System.Linq;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain;
using Translator.Views;

namespace Translator.Presenters
{
    public class TranslatorPresenter : BasePresenter<ITranslatorView>
    {
        private readonly ILanguageMapper _languageMapper = ServiceLocator.Instance.GetService<ILanguageMapper>();
        private readonly IWordMapper _wordMapper = ServiceLocator.Instance.GetService<IWordMapper>();
        protected readonly User User = ServiceLocator.Instance.GetService<User>();

        public TranslatorPresenter()
        {
            View = ServiceLocator.Instance.GetService<ITranslatorView>();
            GetLanguages();
            GetTranslations();
            View.FindTranslations += () => FindTranslations(View.SearchingText, View.Language);

            if (User.IsInRole("Administrator"))
            {
                View.UpdateTranslation += () => UpdateTranslation(View.SelectedTranslation.Id,
                    View.TranslatableText, View.TranslatableLanguage,
                    View.TranslatedText, View.TranslatedLanguage);

                View.AddTranslation += () => AddTranslation(View.TranslatableText, View.TranslatableLanguage,
                    View.TranslatedText, View.TranslatedLanguage);

                View.DeleteTranslation += () => DeleteTranslation(View.SelectedTranslation.Id);
                View.ShowAdminPanel();
            }
        }

        public void GetLanguages()
        {
            View.UpdateLanguagesList(_languageMapper.GetLanguages());
        }

        public void GetTranslations()
        {
            View.UpdateTranslationView(_wordMapper.GetTranslations());
        }

        public void FindTranslations(string searchingText, Language language)
        {
            var translation = _wordMapper.FindTranslation(searchingText, language.Id).ToList();
            if (!translation.Any())
            {
                View.ShowError("Перевод не найден");
            }

            View.UpdateTranslationView(translation);
        }

        public void UpdateTranslation(long translationId, string translatableWord, Language translatableLanguage,
            string translatedWord, Language translatedLanguage)
        {
            var translatable = _wordMapper.Find(translatableWord) ??
                               _wordMapper.Add(new Word(translatableWord, translatableLanguage));
            var translated = _wordMapper.Find(translatedWord) ??
                             _wordMapper.Add(new Word(translatedWord, translatedLanguage));
            _wordMapper.UpdateTranslate(translationId, translatable.Id, translated.Id);
            GetTranslations();
        }

        public void AddTranslation(string translatableWord, Language translatableLanguage, string translatedWord,
            Language translatedLanguage)
        {
            var translatable = _wordMapper.Find(translatableWord) ??
                               _wordMapper.Add(new Word(translatableWord, translatableLanguage));
            var translated = _wordMapper.Find(translatedWord) ??
                             _wordMapper.Add(new Word(translatedWord, translatedLanguage));
            _wordMapper.AddTranslate(translatable.Id, translated.Id);
            GetTranslations();
        }

        public void DeleteTranslation(long translationId)
        {
            _wordMapper.Delete(translationId);
            GetTranslations();
        }
    }
}
