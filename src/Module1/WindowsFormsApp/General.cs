using System;
using System.Windows.Forms;
using LibraryStandard;
using LibraryStandard.Interfaces;

namespace WindowsFormsApp
{
    public partial class General : Form
    {
        private IMessanger _messanger;

        public General()
        {
            InitializeComponent();
            _messanger = new Messanger();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            var userName = userNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(userName))
            {
                errorMessageLabel.Text = "Please enter your name!";
                return;
            }

            errorMessageLabel.Text = string.Empty;
            messageLabel.Text = _messanger.GetGreeting(new[] { userName });
        }
    }
}
