﻿using System.ComponentModel;

namespace Translator.ViewModels
{
    public class TranslationViewModel
    {
        public long Id { get; set; }
        [DisplayName("Слово")]
        public string TranslatableWord { get; set; }
        public long TranslatableWordId { get; set; }
        public long TranslatableLanguageId { get; set; }
        public long TranslatableLanguage { get; set; }
        [DisplayName("Перевод")]
        public string TranslatedWord { get; set; }
        public long TranslatedWordId { get; set; }
        public long TranslatedLanguageId { get; set; }
        public long TranslatedLanguage { get; set; }

        public TranslationViewModel()
        {
            
        }

        public TranslationViewModel(long id, long translatableWordId, string translatableWord, long translatedWordId, string translatedWord)
        {
            Id = id;
            TranslatableWord = translatableWord;
            TranslatableWordId = translatableWordId;
            TranslatedWord = translatedWord;
            TranslatedWordId = translatedWordId;
        }
    }
}
