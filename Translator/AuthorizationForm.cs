using System;
using System.Windows.Forms;
using Translator.Dependencies;
using Translator.Views;

namespace Translator
{
    public partial class AuthorizationForm : Form, IAuthorizationView
    {
        private readonly ApplicationContext _context = ServiceLocator.Instance.GetService<ApplicationContext>();

        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void singIn_Click(object sender, EventArgs e)
        {
            Authorize?.Invoke();
        }

        public string Username
        {
            get => username.Text;
            set => username.Text = value;
        }

        public string Password
        {
            get => password.Text;
            set => password.Text = value;
        }
        public event Action Authorize;

        public new void Show()
        {
            _context.MainForm = this;
            Application.Run(_context);
        }

        public void ShowMessage(string text)
        {
        }

        public void ShowError(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
