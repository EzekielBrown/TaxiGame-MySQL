using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace TaxiGame
{
    public class Tile
    {
        public Tile(int tileID, int column, int row, bool isHomeTile, bool isDropOffTile, int itemID)
        {
            TileID = tileID;
            Column = column;
            Row = row;
            IsHomeTile = isHomeTile;
            IsDropOffTile = isDropOffTile;
            ItemID = itemID;
        }

        public int TileID { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public bool IsHomeTile { get; set; }
        public bool IsDropOffTile { get; set; }
        public int ItemID { get; set; }
    }
}
