namespace Translator.Views
{
    partial class AuthorizationForm
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
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.singIn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(72, 36);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(201, 20);
            this.username.TabIndex = 0;
            this.username.Text = "someuser";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(72, 83);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(201, 20);
            this.password.TabIndex = 1;
            this.password.Text = "123";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(72, 17);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(106, 13);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "Имя пользователя:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(72, 67);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(48, 13);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "Пароль:";
            // 
            // singIn
            // 
            this.singIn.Location = new System.Drawing.Point(72, 124);
            this.singIn.Name = "singIn";
            this.singIn.Size = new System.Drawing.Size(201, 23);
            this.singIn.TabIndex = 4;
            this.singIn.Text = "Войти";
            this.singIn.UseVisualStyleBackColor = true;
            this.singIn.Click += new System.EventHandler(this.singIn_Click);
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 159);
            this.Controls.Add(this.singIn);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Name = "AuthorizationForm";
            this.Text = "Authorization";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button singIn;
    }
}