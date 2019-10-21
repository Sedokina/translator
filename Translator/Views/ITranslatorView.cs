using System;
using System.Collections.Generic;
using Translator.Domain;
using Translator.ViewModels;

namespace Translator.Views
{
    public interface ITranslatorView : IView
    {
        string SearchingText { get; set; }
        string TranslatableText { get; set; }
        Language TranslatableLanguage { get; set; }
        string TranslatedText { get; set; }
        Language TranslatedLanguage { get; set; }
        Language Language { get; set; }
        TranslationViewModel SelectedTranslation { get; set; }
        event Action FindTranslations;
        event Action AddTranslation;
        event Action UpdateTranslation;
        event Action DeleteTranslation;
        void UpdateLanguagesList(IEnumerable<Language> languages);
        void UpdateTranslationView(IEnumerable<Translation> translations);
        void ShowAdminPanel();
    }
}
