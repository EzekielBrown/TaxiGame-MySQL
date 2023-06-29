using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace TaxiGame
{
    public class GameInDB
    {
        public int GameID { get; set; }
        public string Username { get; set; }

        public GameInDB(int gameID, string username)
        {
            GameID = gameID;
            Username = username;
        }
    }
}
