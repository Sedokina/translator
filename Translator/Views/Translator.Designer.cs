namespace Translator.Views
{
    partial class TranslatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.targetLanguage = new System.Windows.Forms.ComboBox();
            this.Translate = new System.Windows.Forms.Button();
            this.wordsGrid = new System.Windows.Forms.DataGridView();
            this.Search = new System.Windows.Forms.TextBox();
            this.EditTranslationButton = new System.Windows.Forms.Button();
            this.translatableTextbox = new System.Windows.Forms.TextBox();
            this.translatedTextbox = new System.Windows.Forms.TextBox();
            this.SaveChanges = new System.Windows.Forms.Button();
            this.AddTranslationButton = new System.Windows.Forms.Button();
            this.DeleteTranslationButton = new System.Windows.Forms.Button();
            this.translatableDropdown = new System.Windows.Forms.ComboBox();
            this.translatedDropdown = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.wordsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // targetLanguage
            // 
            this.targetLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetLanguage.FormattingEnabled = true;
            this.targetLanguage.Location = new System.Drawing.Point(482, 13);
            this.targetLanguage.Name = "targetLanguage";
            this.targetLanguage.Size = new System.Drawing.Size(121, 21);
            this.targetLanguage.TabIndex = 0;
            // 
            // Translate
            // 
            this.Translate.Location = new System.Drawing.Point(713, 12);
            this.Translate.Name = "Translate";
            this.Translate.Size = new System.Drawing.Size(75, 23);
            this.Translate.TabIndex = 1;
            this.Translate.Text = "Перевести";
            this.Translate.UseVisualStyleBackColor = true;
            this.Translate.Click += new System.EventHandler(this.Translate_Click);
            // 
            // wordsGrid
            // 
            this.wordsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wordsGrid.Location = new System.Drawing.Point(12, 39);
            this.wordsGrid.Name = "wordsGrid";
            this.wordsGrid.ReadOnly = true;
            this.wordsGrid.Size = new System.Drawing.Size(776, 370);
            this.wordsGrid.TabIndex = 2;
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(12, 14);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(464, 20);
            this.Search.TabIndex = 3;
            // 
            // EditTranslationButton
            // 
            this.EditTranslationButton.Location = new System.Drawing.Point(12, 415);
            this.EditTranslationButton.Name = "EditTranslationButton";
            this.EditTranslationButton.Size = new System.Drawing.Size(75, 23);
            this.EditTranslationButton.TabIndex = 4;
            this.EditTranslationButton.Text = "Редактировать";
            this.EditTranslationButton.UseVisualStyleBackColor = true;
            this.EditTranslationButton.Visible = false;
            this.EditTranslationButton.Click += new System.EventHandler(this.EditTranslationButton_Click);
            // 
            // translatableTextbox
            // 
            this.translatableTextbox.Location = new System.Drawing.Point(12, 444);
            this.translatableTextbox.Name = "translatableTextbox";
            this.translatableTextbox.Size = new System.Drawing.Size(237, 20);
            this.translatableTextbox.TabIndex = 5;
            this.translatableTextbox.Visible = false;
            // 
            // translatedTextbox
            // 
            this.translatedTextbox.Location = new System.Drawing.Point(12, 470);
            this.translatedTextbox.Name = "translatedTextbox";
            this.translatedTextbox.Size = new System.Drawing.Size(237, 20);
            this.translatedTextbox.TabIndex = 6;
            this.translatedTextbox.Visible = false;
            // 
            // SaveChanges
            // 
            this.SaveChanges.Location = new System.Drawing.Point(255, 415);
            this.SaveChanges.Name = "SaveChanges";
            this.SaveChanges.Size = new System.Drawing.Size(75, 23);
            this.SaveChanges.TabIndex = 7;
            this.SaveChanges.Text = "Сохранить";
            this.SaveChanges.UseVisualStyleBackColor = true;
            this.SaveChanges.Visible = false;
            this.SaveChanges.Click += new System.EventHandler(this.SaveChanges_Click);
            // 
            // AddTranslationButton
            // 
            this.AddTranslationButton.Location = new System.Drawing.Point(93, 415);
            this.AddTranslationButton.Name = "AddTranslationButton";
            this.AddTranslationButton.Size = new System.Drawing.Size(75, 23);
            this.AddTranslationButton.TabIndex = 8;
            this.AddTranslationButton.Text = "Добавить";
            this.AddTranslationButton.UseVisualStyleBackColor = true;
            this.AddTranslationButton.Visible = false;
            this.AddTranslationButton.Click += new System.EventHandler(this.AddTranslationButton_Click);
            // 
            // DeleteTranslationButton
            // 
            this.DeleteTranslationButton.Location = new System.Drawing.Point(174, 415);
            this.DeleteTranslationButton.Name = "DeleteTranslationButton";
            this.DeleteTranslationButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteTranslationButton.TabIndex = 9;
            this.DeleteTranslationButton.Text = "Удалить";
            this.DeleteTranslationButton.UseVisualStyleBackColor = true;
            this.DeleteTranslationButton.Visible = false;
            this.DeleteTranslationButton.Click += new System.EventHandler(this.DeleteTranslationButton_Click);
            // 
            // translatableDropdown
            // 
            this.translatableDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translatableDropdown.FormattingEnabled = true;
            this.translatableDropdown.Location = new System.Drawing.Point(255, 443);
            this.translatableDropdown.Name = "translatableDropdown";
            this.translatableDropdown.Size = new System.Drawing.Size(121, 21);
            this.translatableDropdown.TabIndex = 10;
            this.translatableDropdown.Visible = false;
            // 
            // translatedDropdown
            // 
            this.translatedDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.translatedDropdown.FormattingEnabled = true;
            this.translatedDropdown.Location = new System.Drawing.Point(255, 469);
            this.translatedDropdown.Name = "translatedDropdown";
            this.translatedDropdown.Size = new System.Drawing.Size(121, 21);
            this.translatedDropdown.TabIndex = 11;
            this.translatedDropdown.Visible = false;
            // 
            // TranslatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 495);
            this.Controls.Add(this.translatedDropdown);
            this.Controls.Add(this.translatableDropdown);
            this.Controls.Add(this.DeleteTranslationButton);
            this.Controls.Add(this.AddTranslationButton);
            this.Controls.Add(this.SaveChanges);
            this.Controls.Add(this.translatedTextbox);
            this.Controls.Add(this.translatableTextbox);
            this.Controls.Add(this.EditTranslationButton);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.wordsGrid);
            this.Controls.Add(this.Translate);
            this.Controls.Add(this.targetLanguage);
            this.Name = "TranslatorForm";
            this.Text = "Translator";
            ((System.ComponentModel.ISupportInitialize)(this.wordsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox targetLanguage;
        private System.Windows.Forms.Button Translate;
        private System.Windows.Forms.DataGridView wordsGrid;
        private System.Windows.Forms.TextBox Search;
        private System.Windows.Forms.Button EditTranslationButton;
        private System.Windows.Forms.TextBox translatableTextbox;
        private System.Windows.Forms.TextBox translatedTextbox;
        private System.Windows.Forms.Button SaveChanges;
        private System.Windows.Forms.Button AddTranslationButton;
        private System.Windows.Forms.Button DeleteTranslationButton;
        private System.Windows.Forms.ComboBox translatableDropdown;
        private System.Windows.Forms.ComboBox translatedDropdown;
    }
}

