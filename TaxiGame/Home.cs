using static TaxiGame.DataAccess;

namespace TaxiGame
{
    public partial class Home : Form
    {
        private DAHome daHome;
        private bool isAdmin;
        private Admin _admin;
        private Gameboard _gameboard;
        private string currentUsername;



        public Home(string username)
        {
            InitializeComponent();
            daHome = new DAHome();
            _admin = new Admin(this);
            currentUsername = username;

            OnlinePlayers();
            GameList();

        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            int newGameID = daHome.Create_Game(currentUsername);

            if (newGameID > 0)
            {
                MessageBox.Show("New game created successfully.");

                _gameboard = new Gameboard(this, newGameID, currentUsername);
                _gameboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to create a new game.");
            }
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
            string result = daHome.Log_Out(currentUsername);

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
            buttonKill.Visible = isAdmin;
        }

        private void OnlinePlayers()
        {
            listBoxPlayers.Items.Clear();

            List<PlayerInDB> activePlayers = daHome.Active_User_List();

            foreach (PlayerInDB player in activePlayers)
            {
                listBoxPlayers.Items.Add(player.Username);
            }
        }

        private void GameList()
        {
            List<GameInDB> gameList = daHome.Game_List();

            listBoxGames.Items.Clear();
            foreach (GameInDB game in gameList)
            {
                string gameInfo = $"{game.GameID} - {game.Username}";
                listBoxGames.Items.Add(gameInfo);
            }
        }

        private void buttonJoin_Click(object sender, EventArgs e)
        {
            string selectedGame = listBoxGames.SelectedItem as string;

            if (selectedGame != null)
            {
                int gameID;
                if (int.TryParse(selectedGame.Split('-')[0].Trim(), out gameID))
                {
                    int userID = 1; // remember to fix
                    string result = daHome.Join_Game(gameID, userID);

                    if (result == "Game Joined")
                    {
                        MessageBox.Show("Joined the game successfully.");
                        Gameboard gameboard = new Gameboard(this, gameID, currentUsername);
                        gameboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Failed to join the game.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid game selected.");
                }
            }
            else
            {
                MessageBox.Show("Please select a game to join.");
            }
        }

        private void buttonKill_Click(object sender, EventArgs e)
        {
            if (isAdmin)
            {
                string selectedGame = listBoxGames.SelectedItem as string;

                if (selectedGame != null)
                {
                    int gameID;
                    if (int.TryParse(selectedGame.Split('-')[0].Trim(), out gameID))
                    {
                        bool result = daHome.KillGame(gameID);

                        if (result)
                        {
                            MessageBox.Show("The game has been successfully deleted.");
                            GameList();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the game.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid game selected.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a game to delete.");
                }
            }
            else
            {
                MessageBox.Show("You do not have admin privileges.");
            }
        }

    }
}