namespace TaxiGame
{
    public partial class Login : Form
    {
        private Register _register;
        private DataAccess dataAccess;
        private Home _home;
        private bool isAdmin;

        public Login()
        {
            InitializeComponent();
            _register = new Register();
            dataAccess = new DataAccess();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text;
            string password = textPassword.Text;

            string result = dataAccess.Log_In(username, password, out isAdmin);

            if (result == "Login Successful")
            {
                _home = new Home(username);
                _home.SetAdminStatus(isAdmin);

                this.Hide();
                _home.Show();
            }
            else if (result == "User Exists")
            {
                MessageBox.Show("Incorrect Password");
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonNewUser_Click(object sender, EventArgs e)
        {
            _register.Show();
            this.Hide();
        }
    }
}
