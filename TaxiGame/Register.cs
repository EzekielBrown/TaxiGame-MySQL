using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxiGame
{
    public partial class Register : Form
    {
        private DataAccess dataAccess;
        private Login _login;
        public Register()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text;
            string password = textPassword.Text;
            string email = textEmail.Text;

            string result = dataAccess.New_User(username, password, email);

            if (result == "User Exists")
            {
                MessageBox.Show("Username already exists.");            
            }
            else if (result == "Email Exists")
            {
                MessageBox.Show("Email already exists.");
            }
            else
            {
                MessageBox.Show("Account created successfully!");
                this.Hide();

                _login = new Login();
                _login.ShowDialog();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            _login = new Login();
            _login.Show();

            this.Close();
        }

    }
}
