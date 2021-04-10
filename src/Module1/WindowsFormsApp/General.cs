using System;
using System.Windows.Forms;
using LibraryStandard;
using LibraryStandard.Interfaces;

namespace WindowsFormsApp
{
    public partial class General : Form
    {
        private IMessager _messager;

        public General()
        {
            InitializeComponent();
            _messager = new Messager();
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
            messageLabel.Text = _messager.GetGreeting(userName);
        }
    }
}
