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

            MessageBox.Show(result);
            this.Hide();

            _login = new Login();
            _login.ShowDialog();

            this.Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
