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
using static TaxiGame.DataAccess;

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

            OnlinePlayers();

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
                this.Hide();
                _admin.Show();
            }
            else
            {
                MessageBox.Show("You do not have admin privileges.");
            }
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            string currentUsername = "z";

            string result = dataAccess.Log_Out(currentUsername);

            if (result == "Logout Successful")
            {
                MessageBox.Show("Logout Successful");

                Login loginForm = new Login();
                loginForm.Show();

                this.Close();
            }
            else
            {
                Login loginForm = new Login();
                loginForm.Show();

                this.Close();
            }
        }

        public void SetAdminStatus(bool isAdmin)
        {
            this.isAdmin = isAdmin;
            buttonAdmin.Visible = isAdmin;
        }

        private void OnlinePlayers()
        {
            listBoxPlayers.Items.Clear();

            List<PlayerInDB> activePlayers = dataAccess.Active_User_List();

            foreach (PlayerInDB player in activePlayers)
            {
                listBoxPlayers.Items.Add(player.Username);
            }
        }


    }
}