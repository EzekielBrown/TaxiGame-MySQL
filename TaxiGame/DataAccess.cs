using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TaxiGame
{
    public class DataAccess
    {
        private static String connectionString = "Server=localhost;Port=3306;Database=taxi_game;Uid=root;password=root;";
        private MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

        public string New_User(string pUsername, String pPassword, String pEmail) 
        {
            List<MySqlParameter> p = new List<MySqlParameter>();
            var aP = new MySqlParameter("@Username", MySqlDbType.VarChar, 45);
            aP.Value = pUsername;
            p.Add(aP);

            var bP = new MySqlParameter("@Password", MySqlDbType.VarChar, 45);
            bP.Value = pPassword;
            p.Add(bP);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL registerPlayer(@Username, @Password)", p.ToArray());

            // expecting one table with one row
            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        public class PlayerInDB
        {
            private string _username;
            private string _password;


            public string Username { get => _username; set => _username = value; }
            public string Password { get => _password; set => _password = value; }
        }
        public string Log_In(string pUsername, string pPassword) 
        {
            List<MySqlParameter> aLogin = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            aLogin.Add(aUsername);

            MySqlParameter aPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);
            aPassword.Value = pPassword;
            aLogin.Add(aPassword);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call Login(@Username, @Password)", aLogin.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Log_Out(string pUsername) 
        {
        }

        public string Delete_User(string pUsername)
        {
        }
        public string Active_User_List() 
        {
        }
        public string Create_Game() 
        { 
        }
        public string Game_List() 
        { 
        }
        public string Join_Game(string pUsername) 
        { 
        }
        public string User_Movement(string pUsername, int pTileID) 
        {
        }
        public string Chat_Message(string pUsername, string pMessage) 
        { 
        }
        public string Admin_Edit_user(string pUsername, string pPassword, string pEmail) 
        { 
        }

        public string Admin_New_User(string pUsername, string pPassword, string pEmail) 
        { 
        }
        public string Admin_Delete_User(string pUsername) 
        { 
        }



        
    }

    

}
