using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Translator.Dependencies;
using Translator.Domain;
using Translator.ViewModels;
using Translator.Views;

namespace Translator
{
    public partial class TranslatorForm : Form, ITranslatorView
    {
        private readonly ApplicationContext _context = ServiceLocator.Instance.GetService<ApplicationContext>();

        public string SearchingText
        {
            get => Search.Text;
            set => Search.Text = value;
        }

        public string TranslatableText
        {
            get => translatableTextbox.Text;
            set => translatableTextbox.Text = value;
        }

        public Language TranslatableLanguage
        {
            get => targetLanguage.SelectedItem as Language;
            set => targetLanguage.SelectedItem = value;
        }

        public string TranslatedText
        {
            get => translatedTextbox.Text;
            set => translatedTextbox.Text = value;
        }
        public Language TranslatedLanguage
        {
            get => translatedDropdown.SelectedItem as Language;
            set => translatedDropdown.SelectedItem = value;
        }

        public Language Language
        {
            get => targetLanguage.SelectedItem as Language;
            set => targetLanguage.SelectedItem = value;
        }

        public TranslationViewModel SelectedTranslation { get; set; }

        public TranslatorForm()
        {
            InitializeComponent();
        }

        public event Action FindTranslations;
        public event Action AddTranslation;
        public event Action UpdateTranslation;
        public event Action DeleteTranslation;

        private void Translate_Click(object sender, EventArgs e)
        {
            FindTranslations?.Invoke();
        }

        public void UpdateLanguagesList(IEnumerable<Language> languages)
        {
            targetLanguage.DataSource = new BindingSource(languages, null);
            targetLanguage.DisplayMember = "Name";
            targetLanguage.ValueMember = "Id";

            translatableDropdown.DataSource = new BindingSource(languages, null);
            translatableDropdown.DisplayMember = "Name";
            translatableDropdown.ValueMember = "Id";

            translatedDropdown.DataSource = new BindingSource(languages, null);
            translatedDropdown.DisplayMember = "Name";
            translatedDropdown.ValueMember = "Id";
        }

        public void UpdateTranslationView(IEnumerable<Translation> translations)
        {
            wordsGrid.DataSource = new BindingSource(translations
                .Select(t => new TranslationViewModel(t.Id, t.Translatable.Id, t.Translatable.Text, t.Translated.Id,
                    t.Translated.Text)), null);
            if (wordsGrid.Columns["Id"] != null)
                wordsGrid.Columns["Id"].Visible = false;
            if (wordsGrid.Columns["TranslatableWordId"] != null)
                wordsGrid.Columns["TranslatableWordId"].Visible = false;
            if (wordsGrid.Columns["TranslatedWordId"] != null)
                wordsGrid.Columns["TranslatedWordId"].Visible = false;

        }

        public void ShowAdminPanel()
        {
            translatableDropdown.Visible = true;
            translatedDropdown.Visible = true;
            translatableTextbox.Visible = true;
            translatedTextbox.Visible = true;
            AddTranslationButton.Visible = true;
            DeleteTranslationButton.Visible = true;
            EditTranslationButton.Visible = true;
            SaveChanges.Visible = true;
        }

        public new void Show()
        {
            _context.MainForm = this;
            base.Show();
        }

        public void ShowMessage(string text)
        {
        }

        public void ShowError(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EditTranslationButton_Click(object sender, EventArgs e)
        {
            SelectedTranslation = GetSelected();
            UpdateTextboxes();
        }

        private void AddTranslationButton_Click(object sender, EventArgs e)
        {
            SelectedTranslation = new TranslationViewModel
            {
                TranslatableWord = translatableTextbox.Text,
                TranslatedWord = translatedTextbox.Text
            };
            AddTranslation?.Invoke();
        }

        private void DeleteTranslationButton_Click(object sender, EventArgs e)
        {
            SelectedTranslation = GetSelected();
            DeleteTranslation?.Invoke();
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            SelectedTranslation.TranslatableWord = translatableTextbox.Text;
            SelectedTranslation.TranslatedWord = translatedTextbox.Text;
            UpdateTranslation?.Invoke();
        }

        private void UpdateTextboxes()
        {
            translatableTextbox.Text = SelectedTranslation.TranslatableWord;
            translatedTextbox.Text = SelectedTranslation.TranslatedWord;
        }

        private TranslationViewModel GetSelected()
        {
            TranslationViewModel translation = null;
            if (wordsGrid.SelectedCells.Count > 0)
            {
                var rowIndex = wordsGrid.SelectedCells[0].RowIndex;
                var selectedRow = wordsGrid.Rows[rowIndex];
                translation = new TranslationViewModel(
                    Convert.ToInt64(selectedRow.Cells["Id"].Value),
                    Convert.ToInt64(selectedRow.Cells["TranslatableWordId"].Value),
                    Convert.ToString(selectedRow.Cells["TranslatableWord"].Value),
                    Convert.ToInt64(selectedRow.Cells["TranslatedWordId"].Value),
                    Convert.ToString(selectedRow.Cells["TranslatedWord"].Value)
                );
            }
            return translation;
        }
    }
}