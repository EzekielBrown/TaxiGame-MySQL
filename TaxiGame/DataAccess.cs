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

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL registerUser(@Username, @Password)", p.ToArray());

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
            List<MySqlParameter> logInParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            logInParams.Add(aUsername);

            MySqlParameter aPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);
            aPassword.Value = pPassword;
            logInParams.Add(aPassword);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL LogIn(@Username, @Password)", logInParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Log_Out(string pUsername) 
        {
            List<MySqlParameter> logOutParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            logOutParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL LogOut(@Username)", logOutParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }

        public string Delete_User(string pUsername)
        {
            List<MySqlParameter> deleteUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            deleteUserParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call deleteUser(@Username)", deleteUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<String>("Message");
        }
        public string Active_User_List() 
        {
            List<MySqlParameter> activePlayersParams = new List<MySqlParameter>();

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call getActivePlayers()", activePlayersParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<String>("Message");
        }
        public string Create_Game() 
        {
            List<MySqlParameter> createGameParams = new List<MySqlParameter>();

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL createGame()", createGameParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Game_List() 
        {
            List<MySqlParameter> gameListParams = new List<MySqlParameter>();

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL getGameList()", gameListParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Join_Game(string pUsername) 
        {
            List<MySqlParameter> joinGameParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            joinGameParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL joinGame(@Username)", joinGameParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string User_Movement(string pUsername, int pTileID) 
        {
            List<MySqlParameter> userMovementParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            userMovementParams.Add(aUsername);

            MySqlParameter aTileID = new MySqlParameter("@TileID", MySqlDbType.Int32);
            aTileID.Value = pTileID;
            userMovementParams.Add(aTileID);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL userMovement(@Username, @TileID)", userMovementParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Chat_Message(string pUsername, string pMessage) 
        {
            List<MySqlParameter> chatMessageParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            chatMessageParams.Add(aUsername);

            MySqlParameter aMessage = new MySqlParameter("@Message", MySqlDbType.VarChar, 255);
            aMessage.Value = pMessage;
            chatMessageParams.Add(aMessage);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL chatMessage(@Username, @Message)", chatMessageParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Admin_Edit_user(string pUsername, string pPassword, string pEmail) 
        {
            List<MySqlParameter> adminEditUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            adminEditUserParams.Add(aUsername);

            MySqlParameter aPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 45);
            aPassword.Value = pPassword;
            adminEditUserParams.Add(aPassword);

            MySqlParameter aEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 100);
            aEmail.Value = pEmail;
            adminEditUserParams.Add(aEmail);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL adminEditUser(@Username, @Password, @Email)", adminEditUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }

        public string Admin_New_User(string pUsername, string pPassword, string pEmail) 
        {
            List<MySqlParameter> adminNewUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            adminNewUserParams.Add(aUsername);

            MySqlParameter aPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 45);
            aPassword.Value = pPassword;
            adminNewUserParams.Add(aPassword);

            MySqlParameter aEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 100);
            aEmail.Value = pEmail;
            adminNewUserParams.Add(aEmail);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL adminNewUser(@Username, @Password, @Email)", adminNewUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Admin_Delete_User(string pUsername) 
        {
            List<MySqlParameter> adminDeleteUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            adminDeleteUserParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL adminDeleteUser(@Username)", adminDeleteUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }



        
    }

    

}
