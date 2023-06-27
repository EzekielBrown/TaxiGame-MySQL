using static TaxiGame.DataAccess;

namespace TaxiGame
{
    public partial class Gameboard : Form
    {
        private Home _home;
        private DataAccess dataAccess;
        private int gameID;
        private string username;

        public Gameboard(Home home, int gameID, string username)
        {
            InitializeComponent();
            _home = home;
            dataAccess = new DataAccess();
            this.gameID = gameID;
            this.username = username;
            CreateGameboard();

            dataAccess.SpawnRandomPassenger();

            this.KeyDown += Gameboard_KeyDown;
            this.KeyPreview = true;

        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            _home.Show();
        }

        public void CreateGameboard()
        {
            int userScore = dataAccess.GetUserScore(username);
            score.Text = $"SCORE: {userScore}";

            int numberOfPassengers = dataAccess.GetUserPassengers(username);
            passengers.Text = $"CURRENT PASSENGERS: {numberOfPassengers}";


            List<Tile> tiles = dataAccess.GetTiles();
            int playerCurrentTileID = dataAccess.GetPlayerCurrentTileID(username);

            Dictionary<int, Panel> tilePanels = new Dictionary<int, Panel>();

            foreach (Tile tile in tiles)
            {
                Panel tilePanel = new Panel();
                tilePanel.Size = new Size(50, 50);
                tilePanel.Location = new Point((tile.Column - 1) * 50, (tile.Row - 1) * 50);
                tilePanel.BorderStyle = BorderStyle.FixedSingle;
                tilePanel.Tag = tile.TileID;
                tilePanel.Click += TilePanel_Click;

                if (tile.TileID == playerCurrentTileID)
                {
                    tilePanel.BackColor = Color.Yellow; // The user
                }
                else if (tile.ItemID == 3 && tile.IsHomeTile)
                {
                    tilePanel.BackColor = Color.Green; // Home Tile
                }
                else if (tile.ItemID == 4)
                {
                    tilePanel.BackColor = Color.DarkGray; // Wall
                }
                else if (tile.ItemID == 3 && tile.IsDropOffTile)
                {
                    tilePanel.BackColor = Color.Blue; // Drop off
                }
                else if (tile.ItemID == 3)
                {
                    tilePanel.BackColor = Color.Gray; // Road
                }
                else if (tile.ItemID == 1)
                {
                    tilePanel.BackColor = Color.Pink; // Passenger
                }
                else if (tile.ItemID == 5)
                {
                    tilePanel.BackColor = Color.Black; // Wall that doesn't allow spawns
                }
                else
                {
                    tilePanel.BackColor = tile.IsHomeTile ? Color.Blue : tile.IsDropOffTile ? Color.Green : Color.White;
                }


                tilePanels.Add(tile.TileID, tilePanel);

                panelGame.Controls.Add(tilePanel);
            }
        }

        private void TilePanel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;
            if (clickedPanel != null)
            {
                int clickedTileID = (int)clickedPanel.Tag;

                if (clickedTileID == 16) // checks if the tile is clickable
                {
                    string result = dataAccess.User_Movement(username, clickedTileID);

                    panelGame.Controls.Clear();
                    CreateGameboard();
                }
            }
        }

        private void MovePlayer(int deltaX, int deltaY)
        {
            int playerCurrentTileID = dataAccess.GetPlayerCurrentTileID(username);
            List<Tile> tiles = dataAccess.GetTiles();
            Tile currentTile = tiles.FirstOrDefault(t => t.TileID == playerCurrentTileID);
            bool hasPassenger = dataAccess.HasPassengerInInventory(username);

            if (currentTile == null) return;

            Tile newTile = tiles.FirstOrDefault(t => t.Column == currentTile.Column + deltaX && t.Row == currentTile.Row + deltaY);

            if (newTile != null && (newTile.ItemID == 4 || newTile.ItemID == 5))
            {
                MessageBox.Show("You have crashed. Game over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dataAccess.EndGame(gameID);
                this.Hide();
                _home.Show();
                return;
            }

            if (newTile != null && newTile.ItemID == 1)
            {
                hasPassenger = true;
                dataAccess.SetTileItemID(newTile.TileID, 3);
            }

            int newTileID = newTile?.TileID ?? playerCurrentTileID;

            if (newTileID != playerCurrentTileID)
            {
                string result = dataAccess.User_Movement(username, newTileID);

                if (hasPassenger && newTileID == 19) // if players are on drop off tile
                {
                    int numberOfPassengers = dataAccess.GetUserPassengers(username); // get the number of passengers
                    dataAccess.IncrementPlayerScore(username, 100 * numberOfPassengers); // increase score by 100 per passenger
                    dataAccess.SpawnRandomPassenger(); // spawns another passenger
                    dataAccess.ResetPassengerCount(username); // resets passengers
                }
                panelGame.Controls.Clear();
                CreateGameboard();
            }

            if (newTile != null && newTile.ItemID == 1)
            {
                bool passengerAdded = dataAccess.AddPassengerToInventory(username);
                if (passengerAdded)
                {
                    dataAccess.SetTileItemID(newTile.TileID, 3);
                }
                else
                {
                    MessageBox.Show("Taxi is full!", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void buttonUp_Click(object sender, EventArgs e)
        {
            MovePlayer(0, -1);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            MovePlayer(1, 0);
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            MovePlayer(-1, 0);
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            MovePlayer(0, 1);
        }


        private void Gameboard_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    buttonUp_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.S:
                    buttonDown_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.A:
                    buttonLeft_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.D:
                    buttonRight_Click(sender, e);
                    e.Handled = true;
                    break;
            }
        }

    }
}
