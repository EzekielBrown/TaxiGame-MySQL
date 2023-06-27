using System.Windows.Forms;
using MySql.Data.MySqlClient;

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

                // Check if the clicked tile is the Home tile with TileID 16
                if (clickedTileID == 16)
                {
                    // Call the User_Movement method to update player's position in the database
                    string result = dataAccess.User_Movement(username, clickedTileID);

                    // Refresh the game board to reflect player's new position
                    panelGame.Controls.Clear();
                    CreateGameboard();
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

        private void MovePlayer(int deltaX, int deltaY)
        {
            int playerCurrentTileID = dataAccess.GetPlayerCurrentTileID(username);
            List<Tile> tiles = dataAccess.GetTiles();
            Tile currentTile = tiles.FirstOrDefault(t => t.TileID == playerCurrentTileID);
            bool hasPassenger = dataAccess.HasPassengerInInventory(username); // <---- Note here: Check if the player has a passenger in their inventory

            if (currentTile == null) return;

            // Find the new tile the player is attempting to move to
            Tile newTile = tiles.FirstOrDefault(t => t.Column == currentTile.Column + deltaX && t.Row == currentTile.Row + deltaY);

            // Check if the new tile's ItemID is 4 or 5, if so, show message and end the game
            if (newTile != null && (newTile.ItemID == 4 || newTile.ItemID == 5))
            {
                MessageBox.Show("You have crashed. Game over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dataAccess.EndGame(gameID);
                this.Hide();
                _home.Show();
                return;
            }

            // Check if the player has moved onto a tile with ItemID 1 (picked up a passenger)
            if (newTile != null && newTile.ItemID == 1)
            {
                hasPassenger = true;
                // Update the tile's itemID back to 3 in the database (turn it back into a road)
                dataAccess.SetTileItemID(newTile.TileID, 3);
            }

            int newTileID = newTile?.TileID ?? playerCurrentTileID;

            // Update player position
            if (newTileID != playerCurrentTileID)
            {
                string result = dataAccess.User_Movement(username, newTileID);

                // Check if the player has reached tileID 19 with a passenger
                if (hasPassenger && newTileID == 19) // Checking if the player has reached tileID 19 with a passenger
                {
                    // Increase the player's score by 100
                    dataAccess.IncrementPlayerScore(username, 100);

                    // Spawn another passenger randomly on a road tile
                    dataAccess.SpawnRandomPassenger();

                    // Reset the players passenger counter to 0
                    dataAccess.ResetPassengerCount(username);
                }


                // Refresh the game board to reflect player's new position
                panelGame.Controls.Clear();
                CreateGameboard();
            }

            // Check if the player has moved onto a tile with ItemID 1 (picked up a passenger)
            if (newTile != null && newTile.ItemID == 1)
            {
                // Increment passenger count in inventory
                bool passengerAdded = dataAccess.AddPassengerToInventory(username);
                if (passengerAdded)
                {
                    // Update the tile's itemID back to 3 in the database (turn it back into a road)
                    dataAccess.SetTileItemID(newTile.TileID, 3);
                }
                else
                {
                    // Inform the user that inventory is full
                    MessageBox.Show("Inventory is full!", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }




        private void Gameboard_KeyDown(object sender, KeyEventArgs e)
        {
            // Check which key is pressed and call the appropriate button's Click event handler
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
