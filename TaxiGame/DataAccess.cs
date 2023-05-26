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

        public string New_User() 
        {
            List<MySqlParameter> p = new List<MySqlParameter>();
            var aP = new MySqlParameter("@UserName", MySqlDbType.VarChar, 45);
            aP.Value = pUserName;
            p.Add(aP);

            var bP = new MySqlParameter("@Password", MySqlDbType.VarChar, 45);
            bP.Value = pPassword;
            p.Add(bP);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL registerPlayer(@UserName, @Password)", p.ToArray());

            // expecting one table with one row
            return (aDataSet.Tables[0].Rows[0])["MESSAGE"].ToString();
        }

        public class PlayerInDB
        {
            private string _username;

            public string Username { get => _username; set => _username = value; }
        }
        public string Log_In() 
        {
            List<MySqlParameter> prmLogin = new List<MySqlParameter>();

            MySqlParameter scUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            scUsername.Value = pUsername;
            prmLogin.Add(scUsername);

            MySqlParameter scPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);
            scPassword.Value = pPassword;
            prmLogin.Add(scPassword);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call Login(@Username, @Password)", prmLogin.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<String>("Message");
        }
        public string Log_Out() 
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
        public string Join_Game() 
        { 
        }
        public string User_Movement() 
        {
        }
        public string Chat_Message() 
        { 
        }
        public string Admin_Edit_user() 
        { 
        }

        public string Admin_New_User() 
        { 
        }
        public string Admin_Delete_User() 
        { 
        }
    }

    

}
