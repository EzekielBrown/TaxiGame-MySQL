using static TaxiGame.DataAccess;

namespace TaxiGame
{
    public partial class Admin : Form
    {
        private DataAccess dataAccess;
        private Home homeForm;
        public Admin(Home homeForm)
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.homeForm = homeForm;
        }

        private void buttonGetUser_Click(object sender, EventArgs e)
        {
            int userID;
            if (int.TryParse(textBoxID.Text, out userID))
            {
                PlayerInDB user = dataAccess.GetUserData(userID);

                if (user != null)
                {
                    textBoxUsername.Text = user.Username;
                    textBoxEmail.Text = user.Email;
                    textBoxPassword.Text = user.Password;
                    checkBoxIsLocked.Checked = user.IsLocked;
                    checkBoxIsAdmin.Checked = user.IsAdmin;
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid userID.");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string email = textBoxEmail.Text;
            bool isLocked = checkBoxIsLocked.Checked;
            bool isAdmin = checkBoxIsAdmin.Checked;

            // Call the Admin_Edit_User method from DataAccess to update the user data
            string result = dataAccess.Admin_Edit_User(username, password, email, isLocked, isAdmin);


            if (result == "User Updated")
            {
                MessageBox.Show("User data updated successfully.");
            }
            else
            {
                MessageBox.Show("Failed to update user data.");
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            int userID;
            if (int.TryParse(textBoxID.Text, out userID))
            {
                string result = dataAccess.Admin_Delete_User(userID);

                if (result == "User Deleted")
                {
                    MessageBox.Show("User deleted successfully.");
                }
                else if (result == "User not found")
                {
                    MessageBox.Show("User not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid userID.");
            }
        }




        private void buttonReturn_Click(object sender, EventArgs e)
        {
            homeForm.Show();
            this.Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {

        }
    }
}
