namespace TaxiGame
{
    public partial class Login : Form
    {
        private Login _login;
        private Register _register;
        private DataAccess dataAccess;
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

            string result = dataAccess.Log_In(username, password);

            MessageBox.Show(result);

            if (result == "Login successful") // Assuming this is the success message returned from Log_In method
            {
                // Perform the necessary actions after successful login

                // Close the login form
                this.Close();
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