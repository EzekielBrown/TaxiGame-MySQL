using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TaxiGame
{
    public partial class Home : Form
    {
        private DataAccess dataAccess;
        private bool isAdmin;
        private Admin _admin;
        private Gameboard _gameboard;
        

        public Home()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            _admin = new Admin();
            
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            _gameboard = new Gameboard();
            _gameboard.Show();
            this.Hide();
            

        }

        private void buttonAdmin_Click(object sender, EventArgs e)
        {
            if (isAdmin)
            {
                _admin.Show();
            }
            else
            {
                MessageBox.Show("You do not have admin privileges.");
            }
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            this.Close();

            Login loginForm = new Login();
            loginForm.Show();
        }

        public void SetAdminStatus(bool isAdmin)
        {
            this.isAdmin = isAdmin;
            buttonAdmin.Visible = isAdmin;  // Show the admin button if isAdmin is true
        }


    }
}