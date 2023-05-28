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
            List<MySqlParameter> newUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 45);
            aUsername.Value = pUsername;
            newUserParams.Add(aUsername);

            MySqlParameter aPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 45);
            aPassword.Value = pPassword;
            newUserParams.Add(aPassword);

            MySqlParameter aEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 100);
            aEmail.Value = pEmail;
            newUserParams.Add(aEmail);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL New_User(@Username, @Password, @Email)", newUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }

        public class PlayerInDB
        {
            private string _username;
            private string _password;
            private string _email;
            private bool _isAdmin;
            private bool _isLocked;
            private int _numLoginAttempts;
            private bool _isOnline;
            private int _score;

            public string Username { get => _username; set => _username = value; }
            public string Password { get => _password; set => _password = value; }
            public string Email { get => _email; set => _email = value; }
            public bool IsAdmin { get => _isAdmin; set => _isAdmin = value; }
            public bool IsLocked { get => _isLocked; set => _isLocked = value; }
            public int NumLoginAttempts { get => _numLoginAttempts; set => _numLoginAttempts = value; }
            public bool IsOnline { get => _isOnline; set => _isOnline = value; }
            public int Score { get => _score; set => _score = value; }
        }

        public string Log_In(string pUsername, string pPassword, out bool isAdmin)
        {
            List<MySqlParameter> logInParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@pUsername", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            logInParams.Add(aUsername);

            MySqlParameter aPassword = new MySqlParameter("@pPassword", MySqlDbType.VarChar, 30);
            aPassword.Value = pPassword;
            logInParams.Add(aPassword);

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var aDataSet = MySqlHelper.ExecuteDataset(connection, @"
        SELECT userID, password, numLoginAttempts, isLocked, isAdmin
        FROM tblUser 
        WHERE username = @pUsername", logInParams.ToArray());

                if (aDataSet.Tables[0].Rows.Count > 0)
                {
                    string passwordFromDb = aDataSet.Tables[0].Rows[0].Field<string>("password");
                    int numLoginAttempts = aDataSet.Tables[0].Rows[0].Field<int>("numLoginAttempts");
                    ulong isLocked = aDataSet.Tables[0].Rows[0].Field<ulong>("isLocked");
                    isAdmin = aDataSet.Tables[0].Rows[0].Field<bool>("isAdmin");

                    if (passwordFromDb == pPassword && isLocked == 0)
                    {
                        UpdateUserStatus(connection, aDataSet.Tables[0].Rows[0].Field<int>("userID"));

                        return "Login Successful";
                    }
                    else if (isLocked == 1)
                    {
                        return "Account Locked";
                    }
                    else
                    {
                        UpdateLoginAttempts(connection, aDataSet.Tables[0].Rows[0].Field<int>("userID"), numLoginAttempts + 1);

                        if (numLoginAttempts + 1 >= 5)
                        {
                            LockAccount(connection, aDataSet.Tables[0].Rows[0].Field<int>("userID"));

                            return "Account Locked";
                        }
                        else
                        {
                            return "Login Failed";
                        }
                    }
                }
                else
                {
                    isAdmin = false;
                    return "Login Failed";
                }
            }
        }



        private void UpdateUserStatus(MySqlConnection connection, int userID)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            string query = "UPDATE tblUser SET isOnline = 1, numLoginAttempts = 0 WHERE userID = @UserID";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.ExecuteNonQuery();
            }
        }



        private void UpdateLoginAttempts(MySqlConnection connection, int userID, int numLoginAttempts)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE tblUser SET numLoginAttempts = @NumLoginAttempts WHERE userID = @UserID", mySqlConnection);
            cmd.Parameters.AddWithValue("@NumLoginAttempts", numLoginAttempts);
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.ExecuteNonQuery();
        }

        private void LockAccount(MySqlConnection connection, int userID)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE tblUser SET isLocked = 1 WHERE userID = @UserID", mySqlConnection);
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.ExecuteNonQuery();
        }


        public string Log_Out(string pUsername) 
        {
            List<MySqlParameter> logOutParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            logOutParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Log_Out(@Username)", logOutParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }

        public string Delete_User(string pUsername)
        {
            List<MySqlParameter> deleteUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            deleteUserParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call delete_User(@Username)", deleteUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<String>("Message");
        }
        public string Active_User_List() 
        {
            List<MySqlParameter> activePlayersParams = new List<MySqlParameter>();

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "Call Active_User_List()", activePlayersParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<String>("Message");
        }
        public string Create_Game() 
        {
            List<MySqlParameter> createGameParams = new List<MySqlParameter>();

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Create_Game()", createGameParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Game_List() 
        {
            List<MySqlParameter> gameListParams = new List<MySqlParameter>();

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Game_List()", gameListParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Join_Game(string pUsername) 
        {
            List<MySqlParameter> joinGameParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            joinGameParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Join_Game(@Username)", joinGameParams.ToArray());

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

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL User_Movement(@Username, @TileID)", userMovementParams.ToArray());

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

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Chat_Message(@Username, @Message)", chatMessageParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Admin_Edit_User(string pUsername, string pPassword, string pEmail) 
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

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Admin_Edit_User(@Username, @Password, @Email)", adminEditUserParams.ToArray());

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

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Admin_New_User(@Username, @Password, @Email)", adminNewUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }
        public string Admin_Delete_User(string pUsername) 
        {
            List<MySqlParameter> adminDeleteUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            adminDeleteUserParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL (@Username)", adminDeleteUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }



        
    }

    

}
