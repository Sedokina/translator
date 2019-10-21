using System;
using System.Collections.Generic;
using Translator.Domain.Interfaces;

namespace Translator.Views
{
    public interface ITranslatorView : IView
    {
        string SearchingText { get; set; }
        ILanguage Language { get; set; }
        string TranslatableText { get; set; }
        ILanguage TranslatableLanguage { get; set; }
        string TranslatedText { get; set; }
        ILanguage TranslatedLanguage { get; set; }
        ITranslation SelectedTranslation { get; }
        event Action FindTranslations;
        event Action AddTranslation;
        event Action UpdateTranslation;
        event Action DeleteTranslation;
        event Action GetSelectedTranslation;
        void UpdateLanguagesList(IEnumerable<ILanguage> languages);
        void UpdateTranslationView(IEnumerable<ITranslation> translations);
        void ShowAdminPanel();
    }
}
