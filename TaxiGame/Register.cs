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

         
            if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9]+$")) // Validate username
            {
                MessageBox.Show("Username can only contain letters and numbers.");
                return;
            }
            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 5 characters long.");
                return;
            }


            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^[a-zA-Z0-9]+$")) // validate password
            {
                MessageBox.Show("Password can only contain letters and numbers.");
                return;
            }
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.");
                return;
            }


            if (!email.Contains("@") || email.Length < 5) // validate email
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            string result = dataAccess.New_User(username, password, email);

            if (result == "User Exists") // checks if user exists
            {
                MessageBox.Show("Username already exists.");
            }
            else if (result == "Email Exists") // checks if email exists
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
