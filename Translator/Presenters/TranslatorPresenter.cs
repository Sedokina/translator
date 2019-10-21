using System.Linq;
using Translator.DataMapper.Interfaces;
using Translator.Dependencies;
using Translator.Domain.Domains;
using Translator.Domain.Interfaces;
using Translator.Resources;
using Translator.Views.Interfaces;

namespace Translator.Presenters
{
    public class TranslatorPresenter : BasePresenter<ITranslatorView>
    {
        private readonly ILanguageMapper _languageMapper = ServiceLocator.Instance.GetService<ILanguageMapper>();
        private readonly IWordMapper _wordMapper = ServiceLocator.Instance.GetService<IWordMapper>();
        private readonly ITranslationMapper _translationMapper = ServiceLocator.Instance.GetService<ITranslationMapper>();

        public TranslatorPresenter()
        {
            View = ServiceLocator.Instance.GetService<ITranslatorView>();
            GetLanguages();
            GetTranslations();
            View.FindTranslations += () => FindTranslations(View.SearchingText, View.Language);
           
            if (Credentials.IsInRole(RolesResource.Administrator))
            {
                View.GetSelectedTranslation += () => GetSelectedTranslation(View.SelectedTranslation);

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
            View.UpdateTranslationView(_translationMapper.GetTranslations());
        }

        public void FindTranslations(string searchingText, ILanguage language)
        {
            var translation = _translationMapper.FindTranslation(searchingText, language.Id).ToList();
            if (!translation.Any())
            {
                View.ShowError(MainResources.TranslationNotFound);
            }

            View.UpdateTranslationView(translation);
        }

        public void GetSelectedTranslation(ITranslation translation)
        {
            View.TranslatableText = translation.Translatable.Text;
            View.TranslatableLanguage = new Language {Name = translation.Translatable.Language.Name};
            View.TranslatedText = translation.Translated.Text;
            View.TranslatedLanguage = new Language {Name = translation.Translated.Language.Name};
        }

        public void UpdateTranslation(long translationId, string translatableWord, ILanguage translatableLanguage,
            string translatedWord, ILanguage translatedLanguage)
        {
            var translatable = _wordMapper.Find(translatableWord) ??
                               _wordMapper.Add(new Word(translatableWord, translatableLanguage));
            var translated = _wordMapper.Find(translatedWord) ??
                             _wordMapper.Add(new Word(translatedWord, translatedLanguage));
            _translationMapper.Update(translationId, translatable.Id, translated.Id);
            GetTranslations();
        }

        public void AddTranslation(string translatableWord, ILanguage translatableLanguage, string translatedWord,
            ILanguage translatedLanguage)
        {
            var translatable = _wordMapper.Find(translatableWord) ??
                               _wordMapper.Add(new Word(translatableWord, translatableLanguage));
            var translated = _wordMapper.Find(translatedWord) ??
                             _wordMapper.Add(new Word(translatedWord, translatedLanguage));
            _translationMapper.Add(translatable.Id, translated.Id);
            GetTranslations();
        }

        public void DeleteTranslation(long translationId)
        {
            _translationMapper.Delete(translationId);
            GetTranslations();
        }
    }
}
