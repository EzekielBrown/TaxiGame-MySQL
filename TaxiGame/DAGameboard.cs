using MySql.Data.MySqlClient;
using System.Data;


namespace TaxiGame
{
    public class DAGameboard
    {
        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>();

            try
            {
                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return tiles;
        }



        public int GetPlayerCurrentTileID(string username)
        {
            try
            {
                int currentTileID = 0;

                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }

        public string User_Movement(string pUsername, int pTileID)
        {
            try
            {
                List<MySqlParameter> userMovementParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@p_Username", MySqlDbType.VarChar, 20);
                aUsername.Value = pUsername;
                userMovementParams.Add(aUsername);

                MySqlParameter aTileID = new MySqlParameter("@p_TileID", MySqlDbType.Int32);
                aTileID.Value = pTileID;
                userMovementParams.Add(aTileID);

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL User_Movement(@p_Username, @p_TileID)", userMovementParams.ToArray());

                return aDataSet.Tables[0].Rows[0].Field<string>("Message");
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public void EndGame(int gameID)
        {
            try
            {
                string query = "DELETE FROM tblGame WHERE gameID = @GameID";

                using (MySqlConnection connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GameID", gameID);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public string Chat_Message(string pUsername, string pMessage)
        {
            try
            {
                List<MySqlParameter> chatMessageParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
                aUsername.Value = pUsername;
                chatMessageParams.Add(aUsername);

                MySqlParameter aMessage = new MySqlParameter("@Message", MySqlDbType.VarChar, 255);
                aMessage.Value = pMessage;
                chatMessageParams.Add(aMessage);

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL Chat_Message(@Username, @Message)", chatMessageParams.ToArray());

                return aDataSet.Tables[0].Rows[0].Field<string>("Message");
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public int GetUserScore(string username)
        {
            try
            {
                DataAccess.MySqlConnection.Open();

                string query = "SELECT score FROM tblUser WHERE username = @username";
                using (var command = new MySqlCommand(query, DataAccess.MySqlConnection))
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
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                DataAccess.MySqlConnection.Close();
            }
            return 0;
        }

        public int GetUserPassengers(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    string procedureName = "GetUserPassengers";

                    MySqlCommand command = new MySqlCommand(procedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pUsername", username);
                    command.Parameters.Add("@pPassengerCount", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    int passengerCount = Convert.ToInt32(command.Parameters["@pPassengerCount"].Value);
                    return passengerCount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }


        public void SetTileItemID(int tileID, int itemID)
        {
            try
            {
                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    string procedureName = "SetTileItemID";

                    MySqlCommand command = new MySqlCommand(procedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pTileID", tileID);
                    command.Parameters.AddWithValue("@pItemID", itemID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



        public void IncrementPlayerScore(string username, int amount)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    using (MySqlCommand command = new MySqlCommand("IncrementPlayerScore", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@pUsername", username);
                        command.Parameters.AddWithValue("@pAmount", amount);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void SpawnRandomPassenger()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    using (MySqlCommand command = new MySqlCommand("SpawnRandomPassenger", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public bool AddPassengerToInventory(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    string procedureName = "AddPassengerToInventory";

                    MySqlCommand command = new MySqlCommand(procedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pUsername", username);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public void ResetPassengerCount(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    string procedureName = "ResetPassengerCount";

                    MySqlCommand command = new MySqlCommand(procedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pUsername", username);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public bool HasPassengerInInventory(string username)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    using (MySqlCommand command = new MySqlCommand("HasPassengerInInventory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@pUsername", username);

                        MySqlParameter hasPassengerParam = new MySqlParameter("@pHasPassenger", MySqlDbType.Int32);
                        hasPassengerParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(hasPassengerParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        bool hasPassenger = Convert.ToBoolean(hasPassengerParam.Value);
                        return hasPassenger;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return false;
        }

    }
}
