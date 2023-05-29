using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TaxiGame.DataAccess;

namespace TaxiGame
{
    public partial class Gameboard : Form
    {
        private Home _home;
        private DataAccess dataAccess;
        private int gameID;
        public Gameboard(int gameID)
        {
            InitializeComponent();
            _home = new Home();
            dataAccess = new DataAccess();
            this.gameID = gameID;
        }

        public Gameboard()
        {
            InitializeComponent();
            _home = new Home();
            dataAccess = new DataAccess();
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            _home.Show();

        }

        private void Gameboard_Load(object sender, EventArgs e)
        {
            CreateGameboard();
        }

        public void CreateGameboard()
        {
            List<Tile> tiles = dataAccess.GetTiles();

            foreach (Tile tile in tiles)
            {
                Panel tilePanel = new Panel();
                tilePanel.Size = new Size(50, 50);
                tilePanel.Location = new Point((tile.Column - 1) * 50, (tile.Row - 1) * 50);
                tilePanel.BackColor = tile.IsHomeTile ? Color.Blue : tile.IsDropOffTile ? Color.Green : Color.White;

                panelGame.Controls.Add(tilePanel);
            }
        }

    }
}
