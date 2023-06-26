using static TaxiGame.DataAccess;

namespace TaxiGame
{
    public partial class Gameboard : Form
    {
        private Home _home;
        private DataAccess dataAccess;
        private int gameID;
        private string username; // Keep track of the current username

        public Gameboard(Home home, int gameID, string username) // Added username parameter
        {
            InitializeComponent();
            _home = home;
            dataAccess = new DataAccess();
            this.gameID = gameID;
            this.username = username; // Assign the username
            CreateGameboard();
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            _home.Show();
        }

        public void CreateGameboard()
        {
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
                else if (tile.ItemID == 4)
                {
                    tilePanel.BackColor = Color.Brown; // Wall
                }
                else if (tile.ItemID == 3)
                {
                    tilePanel.BackColor = Color.Gray; // Road
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
                // Call the User_Movement method to update player's position in the database
                string result = dataAccess.User_Movement(username, clickedTileID);

                // Refresh the game board to reflect player's new position
                panelGame.Controls.Clear();
                CreateGameboard();
            }
        }
    }
}
