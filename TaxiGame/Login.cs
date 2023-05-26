namespace TaxiGame
{
    public partial class Login : Form
    {
        private Login _login;
        private Register _register;
        public Login()
        {
            InitializeComponent();
            _register = new Register();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonNewUser_Click(object sender, EventArgs e)
        {
            _register.Show();
        }
    }
}