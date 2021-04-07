using System;
using System.Windows.Forms;
using LibraryStandard;

namespace WindowsFormsApp
{
    public partial class General : Form
    {
        public General()
        {
            InitializeComponent();
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
            var messager = new Messager();
            messageLabel.Text = messager.GetGreetingWithDate(userName);
        }
    }
}
