using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace TaxiGame
{
    public class DataAccess
    {
        private static String connectionString = "Server=localhost;Port=3306;Database=taxi_game;Uid=root;password=root;";
        private MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

        public string New_User(string pUsername, string pPassword, string pEmail)
        {
            bool usernameExists = CheckUsernameExists(pUsername);
            if (usernameExists)
            {
                MessageBox.Show("Username already exists. Please choose a different username.");
                return "User Exists";
            }

            bool emailExists = CheckEmailExists(pEmail);
            if (emailExists)
            {
                MessageBox.Show("Email is already taken. Please choose a different email.");
                return "Email Exists";
            }

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

        private bool CheckUsernameExists(string pUsername)
        {
            List<MySqlParameter> checkUsernameParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 45);
            aUsername.Value = pUsername;
            checkUsernameParams.Add(aUsername);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "SELECT COUNT(*) FROM tblUser WHERE username = @Username", checkUsernameParams.ToArray());
            int count = Convert.ToInt32(aDataSet.Tables[0].Rows[0][0]);

            return count > 0;
        }


        private bool CheckEmailExists(string pEmail)
        {
            List<MySqlParameter> checkEmailParams = new List<MySqlParameter>();

            MySqlParameter aEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 100);
            aEmail.Value = pEmail;
            checkEmailParams.Add(aEmail);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "SELECT COUNT(*) FROM tblUser WHERE email = @Email", checkEmailParams.ToArray());
            int count = Convert.ToInt32(aDataSet.Tables[0].Rows[0][0]);

            return count > 0;
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
            if (connection.State != ConnectionState.Open)
                connection.Open();

            MySqlCommand cmd = new MySqlCommand("UPDATE tblUser SET numLoginAttempts = @NumLoginAttempts WHERE userID = @UserID", connection);
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
        public List<PlayerInDB> Active_User_List()
        {
            List<PlayerInDB> activePlayers = new List<PlayerInDB>();

            List<MySqlParameter> activePlayersParams = new List<MySqlParameter>();

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Active_User_List()", activePlayersParams.ToArray());

            foreach (DataRow row in aDataSet.Tables[0].Rows)
            {
                PlayerInDB player = new PlayerInDB();
                player.Username = row["username"].ToString();

                activePlayers.Add(player);
            }

            return activePlayers;
        }
        public int Create_Game(string pUsername)
        {
            try
            {
                List<MySqlParameter> createGameParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@pUsername", MySqlDbType.VarChar, 20);
                aUsername.Value = pUsername;
                createGameParams.Add(aUsername);

                MySqlParameter aGameID = new MySqlParameter("@pGameID", MySqlDbType.Int32);
                aGameID.Direction = ParameterDirection.Output;
                createGameParams.Add(aGameID);

                mySqlConnection.Open(); // Open connection

                MySqlHelper.ExecuteNonQuery(mySqlConnection, "CALL Create_Game(@pUsername, @pGameID)", createGameParams.ToArray());

                return Convert.ToInt32(aGameID.Value);
            }
            catch (Exception ex)
            {
                return -1; 
            }
            finally
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close(); // Close connection
                }
            }
        }




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




        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT `column`, `row`, homeTile, DropOffTile, tileID, itemID FROM tblTile";

                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int column = Convert.ToInt32(reader["column"]);
                    int row = Convert.ToInt32(reader["row"]);
                    bool isHomeTile = Convert.ToBoolean(reader["homeTile"]);
                    bool isDropOffTile = Convert.ToBoolean(reader["DropOffTile"]);
                    int tileID = Convert.ToInt32(reader["tileID"]);
                    int itemID = Convert.ToInt32(reader["itemID"]);

                    Tile tile = new Tile(tileID, column, row, isHomeTile, isDropOffTile, itemID);
                    tiles.Add(tile);
                }

                reader.Close();
            }

            return tiles;
        }


        public int GetPlayerCurrentTileID(string username)
        {
            int currentTileID = 0;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT tileID FROM tblGame WHERE userID = (SELECT userID FROM tblUser WHERE username = @Username) ORDER BY gameID DESC LIMIT 1";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                var reader = command.ExecuteReader();

                if (reader.Read() && !reader.IsDBNull(0))
                {
                    currentTileID = Convert.ToInt32(reader["tileID"]);
                }

                reader.Close();
            }

            return currentTileID;
        }



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

        public List<GameInDB> Game_List()
        {
            List<GameInDB> games = new List<GameInDB>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT g.gameID, u.username FROM tblGame g INNER JOIN tblUser u ON g.userID = u.userID";
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int gameID = Convert.ToInt32(reader["gameID"]);
                    string username = reader["username"].ToString();

                    GameInDB game = new GameInDB(gameID, username);
                    games.Add(game);
                }

                reader.Close();
            }

            return games;
        }

        public string Join_Game(int gameID, int userID)
        {
            List<MySqlParameter> joinGameParams = new List<MySqlParameter>();

            MySqlParameter aGameID = new MySqlParameter("@pGameID", MySqlDbType.Int32);
            aGameID.Value = gameID;
            joinGameParams.Add(aGameID);

            MySqlParameter aUserID = new MySqlParameter("@pUserID", MySqlDbType.Int32);
            aUserID.Value = userID;
            joinGameParams.Add(aUserID);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Join_Game(@pGameID, @pUserID)", joinGameParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }

        public string User_Movement(string pUsername, int pTileID)
        {
            List<MySqlParameter> userMovementParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@p_Username", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            userMovementParams.Add(aUsername);

            MySqlParameter aTileID = new MySqlParameter("@p_TileID", MySqlDbType.Int32);
            aTileID.Value = pTileID;
            userMovementParams.Add(aTileID);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL User_Movement(@p_Username, @p_TileID)", userMovementParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }

        public void EndGame(int gameID)
        {
            // SQL query to delete the game record from tblGame
            string query = "DELETE FROM tblGame WHERE gameID = @GameID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Add the parameter and its value to the command
                    command.Parameters.AddWithValue("@GameID", gameID);

                    // Open the connection
                    connection.Open();

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
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
        public string Admin_Edit_User(string pUsername, string pPassword, string pEmail, bool pIsLocked, bool pIsAdmin)
        {
            List<MySqlParameter> adminEditUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@pUsername", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            adminEditUserParams.Add(aUsername);

            MySqlParameter aPassword = new MySqlParameter("@pPassword", MySqlDbType.VarChar, 30);
            aPassword.Value = pPassword;
            adminEditUserParams.Add(aPassword);

            MySqlParameter aEmail = new MySqlParameter("@pEmail", MySqlDbType.VarChar, 50);
            aEmail.Value = pEmail;
            adminEditUserParams.Add(aEmail);

            MySqlParameter aIsLocked = new MySqlParameter("@pIsLocked", MySqlDbType.Bit);
            aIsLocked.Value = pIsLocked;
            adminEditUserParams.Add(aIsLocked);

            MySqlParameter aIsAdmin = new MySqlParameter("@pIsAdmin", MySqlDbType.Bit);
            aIsAdmin.Value = pIsAdmin;
            adminEditUserParams.Add(aIsAdmin);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Admin_Edit_User(@pUsername, @pPassword, @pEmail, @pIsLocked, @pIsAdmin)", adminEditUserParams.ToArray());

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
        public string Admin_Delete_User(int userID)
        {
            List<MySqlParameter> adminDeleteUserParams = new List<MySqlParameter>();

            MySqlParameter aUserID = new MySqlParameter("@pUserID", MySqlDbType.Int32);
            aUserID.Value = userID;
            adminDeleteUserParams.Add(aUserID);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Admin_Delete_User(@pUserID)", adminDeleteUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }


        public PlayerInDB GetUserData(int userID)
        {
            List<MySqlParameter> getUserParams = new List<MySqlParameter>();

            MySqlParameter aUserID = new MySqlParameter("@UserID", MySqlDbType.Int32);
            aUserID.Value = userID;
            getUserParams.Add(aUserID);

            var aDataSet = MySqlHelper.ExecuteDataset(mySqlConnection, "CALL Get_User_Data(@UserID)", getUserParams.ToArray());

            if (aDataSet.Tables[0].Rows.Count > 0)
            {
                PlayerInDB user = new PlayerInDB();
                user.Username = aDataSet.Tables[0].Rows[0].Field<string>("Username");
                user.Password = aDataSet.Tables[0].Rows[0].Field<string>("Password");
                user.Email = aDataSet.Tables[0].Rows[0].Field<string>("Email");
                // Set other user properties

                return user;
            }

            return null;
        }

        public int GetUserScore(string username)
        {
            try
            {
                mySqlConnection.Open();

                string query = "SELECT score FROM tblUser WHERE username = @username";
                using (var command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log or handle exception
            }
            finally
            {
                mySqlConnection.Close();
            }
            return 0;
        }

        public void SetTileItemID(int tileID, int itemID)
        {
            string query = "UPDATE tblTile SET itemID = @ItemID WHERE tileID = @TileID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TileID", tileID);
                    command.Parameters.AddWithValue("@ItemID", itemID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void IncrementPlayerScore(string username, int amount)
        {
            string query = "UPDATE tblUser SET score = score + @Amount WHERE username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Amount", amount);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void SpawnRandomPassenger()
        {
            string query = "UPDATE tblTile SET itemID = 1 WHERE itemID = 3 ORDER BY RAND() LIMIT 1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool AddPassengerToInventory(string username)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE tblInventory SET passengerCount = passengerCount + 1 WHERE userID = (SELECT userID FROM tblUser WHERE username = @username) AND passengerCount < 3";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public void ResetPassengerCount(string username)
        {
            string query = @"
        UPDATE tblInventory
        SET passengerCount = 0
        WHERE userID = (SELECT userID FROM tblUser WHERE username = @Username)
    ";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool HasPassengerInInventory(string username)
        {
            string query = @"
        SELECT passengerCount
        FROM tblInventory
        WHERE userID = (SELECT userID FROM tblUser WHERE username = @Username)
    ";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int passengerCount = reader.GetInt32("passengerCount");
                            return passengerCount > 0;
                        }
                    }
                }
            }

            return false;
        }


    }



}
