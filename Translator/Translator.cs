using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Translator.Dependencies;
using Translator.Domain;
using Translator.Domain.Interfaces;
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

        public ILanguage Language
        {
            get => targetLanguage.SelectedItem as Language;
            set => targetLanguage.SelectedItem = value;
        }

        public string TranslatableText
        {
            get => translatableTextbox.Text;
            set => translatableTextbox.Text = value;
        }

        public ILanguage TranslatableLanguage
        {
            get => targetLanguage.SelectedItem as Language;
            set => translatableDropdown.SelectedIndex = translatableDropdown.FindString(value.Name);
        }

        public string TranslatedText
        {
            get => translatedTextbox.Text;
            set => translatedTextbox.Text = value;
        }

        public ILanguage TranslatedLanguage
        {
            get => translatedDropdown.SelectedItem as Language;
            set => translatedDropdown.SelectedIndex = translatedDropdown.FindString(value.Name);
        }

        public ITranslation SelectedTranslation => GetGridSelectedItemIt();

        public TranslatorForm()
        {
            InitializeComponent();
        }

        public event Action FindTranslations;
        public event Action AddTranslation;
        public event Action UpdateTranslation;
        public event Action DeleteTranslation;
        public event Action GetSelectedTranslation;

        private void Translate_Click(object sender, EventArgs e)
        {
            FindTranslations?.Invoke();
        }

        public void UpdateLanguagesList(IEnumerable<ILanguage> languages)
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

        public void UpdateTranslationView(IEnumerable<ITranslation> translations)
        {
            wordsGrid.DataSource = new BindingSource(translations
                .Select(t => new
                {
                    Id = t.Id,
                    TranslatableId = t.Translatable.Id,
                    TranslatableLanguage = t.Translatable.Language.Name,
                    TranslatableWord = t.Translatable.Text,
                    TranslatedId = t.Translated.Id,
                    TranslatedLanguage = t.Translated.Language.Name,
                    TranslatedWord = t.Translated.Text
                }), null);
            if (wordsGrid.Columns["Id"] != null)
                wordsGrid.Columns["Id"].Visible = false;
            if (wordsGrid.Columns["TranslatableId"] != null)
                wordsGrid.Columns["TranslatableId"].Visible = false;
            if (wordsGrid.Columns["TranslatedId"] != null)
                wordsGrid.Columns["TranslatedId"].Visible = false;
            if (wordsGrid.Columns["TranslatableLanguage"] != null)
                wordsGrid.Columns["TranslatableLanguage"].Visible = false;
            if (wordsGrid.Columns["TranslatedLanguage"] != null)
                wordsGrid.Columns["TranslatedLanguage"].Visible = false;
            if (wordsGrid.Columns["TranslatableWord"] != null)
                wordsGrid.Columns["TranslatableWord"].HeaderText = "Слово";
            if (wordsGrid.Columns["TranslatedWord"] != null)
                wordsGrid.Columns["TranslatedWord"].HeaderText = "Перевод";
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
            GetSelectedTranslation?.Invoke();
        }

        private void AddTranslationButton_Click(object sender, EventArgs e)
        {
            AddTranslation?.Invoke();
        }

        private void DeleteTranslationButton_Click(object sender, EventArgs e)
        {
            DeleteTranslation?.Invoke();
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            UpdateTranslation?.Invoke();
        }
        
        private ITranslation GetGridSelectedItemIt()
        {
            ITranslation translation = null;
            if (wordsGrid.SelectedCells.Count > 0)
            {
                var rowIndex = wordsGrid.SelectedCells[0].RowIndex;
                var selectedRow = wordsGrid.Rows[rowIndex];
                translation = new Translation(
                    Convert.ToInt64(selectedRow.Cells["Id"].Value),
                    new Word(Convert.ToInt64(selectedRow.Cells["TranslatableId"].Value),
                        Convert.ToString(selectedRow.Cells["TranslatableWord"].Value),
                        new Language { Name = Convert.ToString(selectedRow.Cells["TranslatableLanguage"].Value) }
                    ),
                    new Word(Convert.ToInt64(selectedRow.Cells["TranslatedId"].Value),
                        Convert.ToString(selectedRow.Cells["TranslatedWord"].Value),
                        new Language { Name = Convert.ToString(selectedRow.Cells["TranslatedLanguage"].Value) }
                    )
                );
            }

            return translation;
        }
    }
}